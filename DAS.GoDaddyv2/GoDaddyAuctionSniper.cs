using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using DAS.DAL2;
using DAS.Domain.GoDaddy;
using DAS.Domain.GoDaddy.Users;
using DAS.Domain.Users;
using DeathByCaptcha;
using HtmlAgilityPack;
using Lunchboxweb;
using Exception = System.Exception;

namespace DAS.GoDaddyv2
{
    public class GoDaddyAuctionSniper : HttpBase
    {
        private readonly IGoDaddySession sessionDetails;
        public IUserRepository UserRepository;

        public GoDaddyAuctionSniper(string userName, IUserRepository userRepository)
        {
            UserRepository = userRepository;
            sessionDetails = userRepository.GetSessionDetails(userName);
        }

        public bool CaptchaOverload { get; set; }


        public DateTime GetPacificTime
        {
            get
            {
                var tzi = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
                var localDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi);

                return localDateTime;
            }
        }

        //public bool LoggedIn()
        //{
        //    var memberInfo = CookieContainer.GetType().GetField("m_domainTable", BindingFlags.Instance | BindingFlags.NonPublic);
        //    if (memberInfo == null) return false;
        //    var k = (Hashtable)memberInfo.GetValue(CookieContainer);
        //    foreach (DictionaryEntry element in k)
        //    {
        //        var fieldInfo = element.Value.GetType().GetField("m_list", BindingFlags.Instance | BindingFlags.NonPublic);
        //        if (fieldInfo == null) continue;
        //        var l = (SortedList)fieldInfo.GetValue(element.Value);
        //        if ((from object e in l select (CookieCollection)((DictionaryEntry)e).Value).Any(cl => cl.Cast<Cookie>().Any(fc => fc.Secure && fc.Domain == "godaddy.com" && fc.Name == "auth_idp")))
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}

        public bool LoggedIn()
        {
            return LoggedIn(Get("https://auctions.godaddy.com/"));
        }

        public bool LoggedIn(string html)
        {
            var hdoc = HtmlDocument(html);
            if ((QuerySelector(hdoc.DocumentNode, "input[id='hidShopperId']") != null) &&
                (QuerySelector(hdoc.DocumentNode, "input[id='hidShopperId']").Attributes["value"].Value != ""))
                return true;
            return false;
        }

        public bool Login(int attempNo = 0, string username = null, string password = null)
        {
            var responseData = Get("https://auctions.godaddy.com/");
            if (LoggedIn(responseData))
                return true;
            var key = GetSubString(responseData, "SPKey=", "\"");
            var loginurl = $"https://idp.godaddy.com/login.aspx?SPKey={key}";
            var hdoc = HtmlDocument(Get(loginurl));
            if (QuerySelector(hdoc.DocumentNode, "img[class='LBD_CaptchaImage']") == null)
            {
                var login = "https://sso.godaddy.com/?app=auctions";
                var loginData =
                    $"name={Uri.EscapeDataString(username ?? sessionDetails.GoDaddyAccount.Username)}&password={Uri.EscapeDataString(password ?? sessionDetails.GoDaddyAccount.Password)}";
                return LoggedIn(Post(login, loginData));
            }

            if (QuerySelector(hdoc.DocumentNode, "img[class='LBD_CaptchaImage']") != null)
            {
                var captchaId =
                    QuerySelector(hdoc.DocumentNode, "input[id='LBD_VCID_idpCatpcha']").Attributes["value"]
                        .Value;
                var imagedata =
                    GetImage(QuerySelector(hdoc.DocumentNode, "img[class='LBD_CaptchaImage']").Attributes["src"].Value);

                try
                {
                    imagedata.Save(
                        Path.Combine(Path.GetTempPath(), username ?? sessionDetails.GoDaddyAccount.Username + ".jpg"),
                        ImageFormat.Jpeg);
                    var user = sessionDetails.DeathByCapture.Username;
                    var pass = sessionDetails.DeathByCapture.Password;
                    Client client = new SocketClient(user, pass);

                    //var balance = client.GetBalance();
                    var captcha =
                        client.Decode(
                            Path.Combine(Path.GetTempPath(), sessionDetails.GoDaddyAccount.Username + ".jpg"), 20);
                    if (null != captcha)
                    {
                        Console.WriteLine(@"CAPTCHA {0} solved: {1}", captcha.Id, captcha.Text);
                        var capturetext = captcha.Text;

                        var view = ExtractViewStateSearch(hdoc.DocumentNode.InnerHtml);
                        var postData =
                            $"__VIEWSTATE={view}&Login%24userEntryPanel2%24UsernameTextBox={sessionDetails.GoDaddyAccount.Username}&Login%24userEntryPanel2%24PasswordTextBox={sessionDetails.GoDaddyAccount.Password}&captcha_value={capturetext}&LBD_VCID_idpCatpcha={captchaId}&Login%24userEntryPanel2%24LoginImageButton.x=0&Login%24userEntryPanel2%24LoginImageButton.y=0";

                        Post(loginurl, postData);

                        if (!LoggedIn())
                            client.Report(captcha);

                        return LoggedIn();
                    }
                }
                catch (Exception e)
                {
                    UserRepository.LogError(e.Message);
                    CaptchaOverload = true;
                }
            }
            else
            {
                return LoggedIn();
            }

            if (attempNo < 3)
                Login(attempNo + 1);

            return false;
        }

        private AuctionSearch GenerateAuctionSearch()
        {
            var p = new AuctionSearch {AuctionID = Guid.NewGuid()};
            return p;
        }

        public bool WinCheck(string domainName)
        {
            if (!LoggedIn())
                Login();

            const string url = "https://auctions.godaddy.com/trpMessageHandler.aspx ";
            var postData =
                $"sec=Wo&sort=6&dir=D&page=1&rpp=50&at=0&maadv=0|{domainName}|||&rnd={RandomDouble():0.00000000000000000}";
            var data = Post(url, postData);

            return data.Contains(domainName);
        }

        public bool WatchList()
        {
            HtmlDocument(Post("https://auctions.godaddy.com/trpMessageHandler.aspx",
                $"sec=Wa&sort=&dir=&page=1&rpp=50&at=0&rnd={RandomDouble():0.00000000000000000}"));
            return true;
        }

        private static double RandomDouble()
        {
            var rand = new Random();
            return rand.NextDouble()*Math.Abs(1 - 0) + 0;
        }

        private HtmlDocument GetAuctionDetails(string auctionNo)
        {
            var data = Post("https://auctions.godaddy.com/trpMessageHandler.aspx", $"ad={auctionNo}&type=Search");
            var hdoc = HtmlDocument(data);

            return hdoc;
        }

        public DateTime GetEndDate(string auctionNo)
        {
            var endDate = GetPacificTime;
            var details = GetAuctionDetails(auctionNo);
            if (QuerySelector(details.DocumentNode, "span.OneLinkNoTx") != null)
                endDate = QuerySelector(details.DocumentNode, "span.OneLinkNoTx").InnerText.Contains("PM") &&
                          (DateTime.Parse(
                               QuerySelector(details.DocumentNode, "span.OneLinkNoTx")
                                   .InnerText.Replace("AM", "")
                                   .Replace("PM", "")
                                   .Replace("(PST)", "")
                                   .Replace("(PDT)", "")
                                   .Trim(), new CultureInfo("en-US", false)).Hour < 12)
                    ? DateTime.Parse(
                        QuerySelector(details.DocumentNode, "span.OneLinkNoTx")
                            .InnerText.Replace("AM", "")
                            .Replace("PM", "")
                            .Replace("(PST)", "")
                            .Replace("(PDT)", "")
                            .Trim(), new CultureInfo("en-US", false)).AddHours(12)
                    : DateTime.Parse(
                        QuerySelector(details.DocumentNode, "span.OneLinkNoTx")
                            .InnerText.Replace("AM", "")
                            .Replace("PM", "")
                            .Replace("(PST)", "")
                            .Replace("(PDT)", "")
                            .Trim(), new CultureInfo("en-US", false));
            else if (QuerySelector(details.DocumentNode, "td[style*=background-color]") != null)
                endDate = QuerySelector(details.DocumentNode, "td[style*=background-color]").InnerText.Contains("PM") &&
                          (DateTime.Parse(
                               QuerySelector(details.DocumentNode, "td[style*=background-color]")
                                   .InnerText.Replace("AM", "")
                                   .Replace("PM", "")
                                   .Replace("(PST)", "")
                                   .Replace("(PDT)", "")
                                   .Trim(), new CultureInfo("en-US", false)).Hour < 12)
                    ? DateTime.Parse(
                        QuerySelector(details.DocumentNode, "td[style*=background-color]")
                            .InnerText.Replace("AM", "")
                            .Replace("PM", "")
                            .Replace("(PST)", "")
                            .Replace("(PDT)", "")
                            .Trim(), new CultureInfo("en-US", false)).AddHours(12)
                    : DateTime.Parse(
                        QuerySelector(details.DocumentNode, "td[style*=background-color]")
                            .InnerText.Replace("AM", "")
                            .Replace("PM", "")
                            .Replace("(PST)", "")
                            .Replace("(PDT)", "")
                            .Trim(), new CultureInfo("en-US", false));

            return endDate;
        }

        public IEnumerable<Auction> Search(string searchText)
        {
            const string searchString = "https://auctions.godaddy.com/trpSearchResults.aspx";
            var auctions = new List<Auction>();

            Post(searchString,
                "action=review_selected_add&items=192850666_O_X_0|193219958_O_X_0|193032048_O_X_0|193056051_O_X_0|193248018_O_X_0|193167331_O_X_0|193023547_O_X_0|193176746_O_X_0|193047816_O_X_0|193158859_O_X_0|193197911_O_X_0|193228191_O_X_0|193258640_O_X_0|205033503_B_X_0|134260062_O_X_0|149601237_O_X_0|200260973_O_X_0|194376570_O_X_0|175758453_O_X_0|175821926_O_X_0|175842790_O_X_0|175786285_O_X_0|175787829_O_X_0|175919481_O_X_0|175808968_O_X_0|204762728_B_X_0|204762729_B_X_0|204762731_B_X_0|204762732_B_X_0|204762733_B_X_0|204762734_B_X_0|204762795_B_X_0|4862717_O_X_0|4862726_O_X_0|10304393_O_X_0|17118608_O_X_0|192546044_N_X_0|186975223_O_X_0|192678574_O_X_0|133603268_O_X_0|59310076_O_X_0|120001859_O_X_0|186975467_N_X_0|180978293_O_X_0|180978306_O_X_0|133606361_O_X_0|140541323_O_X_0|89826572_O_X_0|89827074_O_X_0|89827145_O_X_0|&rnd=0.26743095430817165&YmWocpF=d488ebe");
            var doc = HtmlDocument(Post(searchString,
                $"t=16&action=search&hidAdvSearch=ddlAdvKeyword:1|txtKeyword:{searchText}|ddlCharacters:0|txtCharacters:|txtMinTraffic:|txtMaxTraffic:|txtMinDomainAge:|txtMaxDomainAge:|txtMinPrice:|txtMaxPrice:|ddlCategories:0|chkAddBuyNow:false|chkAddFeatured:false|chkAddDash:true|chkAddDigit:true|chkAddWeb:false|chkAddAppr:false|chkAddInv:false|chkAddReseller:false|ddlPattern1:|ddlPattern2:|ddlPattern3:|ddlPattern4:|chkSaleOffer:false|chkSalePublic:true|chkSaleExpired:false|chkSaleCloseouts:false|chkSaleUsed:false|chkSaleBuyNow:false|chkSaleDC:false|chkAddOnSale:false|ddlAdvBids:0|txtBids:|txtAuctionID:|ddlDateOffset:&rtr=5&baid=-1&searchDir=1&rnd={RandomDouble():0.00000000000000000}&YmWocpF=d488ebe"));

            if (QuerySelectorAll(doc.DocumentNode, "tr.srRow2, tr.srRow1") != null)
                foreach (var node in QuerySelectorAll(doc.DocumentNode, "tr.srRow2, tr.srRow1"))
                    if ((QuerySelector(node, "span.OneLinkNoTx") != null) &&
                        (QuerySelector(node, "td:nth-child(5)") != null))
                    {
                        var auction = GenerateAuctionSearch();
                        auction.DomainName = HtmlDecode(QuerySelector(node, "span.OneLinkNoTx").InnerText);
                        Console.WriteLine(auction.DomainName);

                        auction.BidCount =
                            TextModifier.TryParse_INT(
                                HtmlDecode(QuerySelector(node, "td:nth-child(5)")
                                    .FirstChild.InnerHtml.Replace("&nbsp;", "")));
                        auction.Traffic =
                            TextModifier.TryParse_INT(
                                HtmlDecode(QuerySelector(node, "td:nth-child(5) > td").InnerText.Replace("&nbsp;", "")));
                        auction.Valuation =
                            TextModifier.TryParse_INT(
                                HtmlDecode(
                                    QuerySelector(node, "td:nth-child(5) > td:nth-child(2)")
                                        .InnerText.Replace("&nbsp;", "")));
                        auction.Price =
                            TextModifier.TryParse_INT(
                                HtmlDecode(QuerySelector(node, "td:nth-child(5) > td:nth-child(3)").InnerText)
                                    .Replace("$", "")
                                    .Replace(",", "")
                                    .Replace("C", ""));

                        try
                        {
                            if (QuerySelector(node, "td:nth-child(5) > td:nth-child(4) > div") != null)
                            {
                                if (
                                    HtmlDecode(QuerySelector(node, "td:nth-child(5) > td:nth-child(4) > div").InnerText)
                                        .Contains("Buy Now for"))
                                    auction.BuyItNow =
                                        TextModifier.TryParse_INT(
                                            Regex.Split(
                                                    HtmlDecode(
                                                        QuerySelector(node, "td:nth-child(5) > td:nth-child(4) > div")
                                                            .InnerText), "Buy Now for")[1].Trim()
                                                .Replace(",", "")
                                                .Replace("$", ""));
                            }
                            else
                            {
                                auction.BuyItNow = 0;
                            }
                        }
                        catch (Exception)
                        {
                            auction.BuyItNow = 0;
                        }

                        if ((QuerySelector(node, "td:nth-child(5) > td:nth-child(5)") != null) &&
                            HtmlDecode(QuerySelector(node, "td:nth-child(5) > td:nth-child(5)").InnerHtml)
                                .Contains("Bid $"))
                            auction.MinBid =
                                TextModifier.TryParse_INT(
                                    GetSubString(
                                        HtmlDecode(QuerySelector(node, "td:nth-child(5) > td:nth-child(5)").InnerHtml),
                                        "Bid $", " or more").Trim().Replace(",", "").Replace("$", ""));

                        if ((QuerySelector(node, "td:nth-child(5) > td:nth-child(5)") != null) &&
                            !HtmlDecode(QuerySelector(node, "td:nth-child(5) > td:nth-child(5)").InnerHtml)
                                .Contains("Bid $"))
                            auction.EstimateEndDate =
                                GenerateEstimateEnd(QuerySelector(node, "td:nth-child(5) > td:nth-child(5)"));

                        if ((QuerySelector(node, "td:nth-child(5) > td:nth-child(4)") != null) &&
                            HtmlDecode(QuerySelector(node, "td:nth-child(5) > td:nth-child(4)").InnerHtml)
                                .Contains("Bid $"))
                            auction.MinBid =
                                TextModifier.TryParse_INT(
                                    GetSubString(
                                        HtmlDecode(QuerySelector(node, "td:nth-child(5) > td:nth-child(4)").InnerHtml),
                                        "Bid $", " or more").Trim().Replace(",", "").Replace("$", ""));

                        if (QuerySelector(node, "td > div > span") != null)
                            foreach (
                                var item in
                                GetSubStrings(QuerySelector(node, "td > div > span").InnerHtml, "'Offer $", " or more"))
                                auction.MinOffer = TextModifier.TryParse_INT(item.Replace(",", ""));

                        auction.EndDate = GetPacificTime;
                        foreach (var item in GetSubStrings(node.InnerHtml, "ShowAuctionDetails('", "',"))
                        {
                            auction.AuctionRef = item;
                            break;
                        }

                        if (auction.MinBid > 0)
                            auctions.Add(new Auction(auction.AuctionID, auction.EndDate, auction.EstimateEndDate,
                                auction.DomainName, auction.AuctionRef, auction.BidCount, auction.MinBid, auction.MyBid,
                                auction.Processed, auction.Traffic));
                    }

            return auctions;
        }

        private DateTime GenerateEstimateEnd(HtmlNode node)
        {
            var estimateEnd = GetPacificTime;
            if (node.InnerText == null) return estimateEnd;
            var vals = HtmlDecode(node.InnerText).Trim().Split(' ');

            foreach (var item in vals)
                if (item.Contains("D"))
                    estimateEnd = estimateEnd.AddDays(double.Parse(item.Replace("D", "")));
                else if (item.Contains("H"))
                    estimateEnd = estimateEnd.AddHours(double.Parse(item.Replace("H", "")));
                else if (item.Contains("M"))
                    estimateEnd = estimateEnd.AddMinutes(double.Parse(item.Replace("M", "")));

            return estimateEnd;
        }

        private bool UnavailableCheck(string auctionRef)
        {
            var data = Get($"https://auctions.godaddy.com/trpItemListing.aspx?miid={auctionRef}");
            return data.Contains("LONGER AVAILABLE THROUGH AUCTION");
        }

        public void PlaceBid(Auction auction)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            if (UnavailableCheck(auction.AuctionRef))
            {
                UserRepository.AddHistoryRecord("The site is no longer available through auction, bid cancelled",
                    auction.AuctionId);
                return;
            }

            UserRepository.AddHistoryRecord("Logging In", auction.AuctionId);

            if (Login())
            {
                UserRepository.AddHistoryRecord("Setting Max Bid: " + auction.MyBid, auction.AuctionId);

                var url = "https://auctions.godaddy.com/trpMessageHandler.aspx";
                var postData = "q=ReviewDomains";
                var responseData = Post(url, postData);
                if (responseData.Contains("Item is closed"))
                {
                    UserRepository.AddHistoryRecord("Bid Process Ended - The item has been closed", auction.AuctionId);
                    return;
                }
                if (responseData.Contains("ERROR: Bid must be a minimum of"))
                {
                    UserRepository.AddHistoryRecord("Bid Process Ended - Your max bid is already too small to place",
                        auction.AuctionId);
                    return;
                }
                if (responseData.Contains("You are currently blocked from bidding due to unpaid items"))
                {
                    UserRepository.AddHistoryRecord("GoDaddy reports you are blocked from bidding due to unpaid items",
                        auction.AuctionId);
                    return;
                }
                if (responseData.Contains("ERROR:"))
                    UserRepository.AddHistoryRecord("Error reported on processing", auction.AuctionId);

                //KeepAlive
                Get("https://auctions.godaddy.com/trpMessageHandler.aspx?keepAlive=1");
                Get("https://idp.godaddy.com/KeepAlive.aspx?SPKey=GDDNAEB002");

                url = "https://img.godaddy.com/pageevents.aspx?page_name=/trphome.aspx&ci=37022" +
                      "&eventtype=click&ciimpressions=&usrin=&relativeX=659&relativeY=325&absoluteX=659&" +
                      $"absoluteY=1102&r={RandomDouble():0.00000000000000000}&comview=0";
                Get(url);

                url = @"https://auctions.godaddy.com/trpItemListingReview.aspx";
                postData = "__VIEWSTATE=" + "&hidAdvSearch=ddlAdvKeyword%3A1%7CtxtKeyword%3Aportal" +
                           "%7CddlCharacters%3A0%7CtxtCharacters%3A%7CtxtMinTraffic%3A%7CtxtMaxTraffic%3A%" +
                           "7CtxtMinDomainAge%3A%7CtxtMaxDomainAge%3A%7CtxtMinPrice%3A%7CtxtMaxPrice%3A%7Cdd" +
                           "lCategories%3A0%7CchkAddBuyNow%3Afalse%7CchkAddFeatured%3Afalse%7CchkAddDash%3Atrue" +
                           "%7CchkAddDigit%3Atrue%7CchkAddWeb%3Afalse%7CchkAddAppr%3Afalse%7CchkAddInv%3Afalse%7" +
                           "CchkAddReseller%3Afalse%7CddlPattern1%3A%7CddlPattern2%3A%7CddlPattern3%3A%7CddlPattern4" +
                           "%3A%7CchkSaleOffer%3Afalse%7CchkSalePublic%3Atrue%7CchkSaleExpired%3Afalse%7CchkSaleCloseouts" +
                           "%3Afalse%7CchkSaleUsed%3Afalse%7CchkSaleBuyNow%3Afalse%7CchkSaleDC%3Afalse%7CchkAddOnSale" +
                           "%3Afalse%7CddlAdvBids%3A0%7CtxtBids%3A%7CtxtAuctionID%3A%7CddlDateOffset%3A%7CddlRecordsPerPageAdv" +
                           "%3A3&hidADVAction=p_&txtKeywordContext=&ddlRowsToReturn=3&hidAction=&hidItemsAddedToCart=" +
                           "&hidGetMemberInfo=&hidShopperId=46311038&hidValidatedMemberInfo=&hidCheckedDomains=&hidMS90483566" +
                           "=O&hidMS86848023=O&hidMS70107049=O&hidMS91154790=O&hidMS39351987=O&hidMS94284110=O&hidMS53775077=" +
                           "O&hidMS75408187=O&hidMS94899096=B&hidMS94899097=B&hidMS94899098=B&hidMS94899099=B&hidMS94937468=" +
                           $"B&hidMS95047168=B&hidMS{auction.AuctionRef}=B&hid_Agree1=on";

                UserRepository.AddHistoryRecord("Bid Process Completed", auction.AuctionId);
                var hdoc = HtmlDocument(Post(url, postData));
                var confirmed = false;
                foreach (var items in QuerySelectorAll(hdoc.DocumentNode, "tr"))
                {
                    if (items.InnerHtml.Contains(auction.AuctionRef) && items.InnerHtml.Contains("the high bidder"))
                    {
                        confirmed = true;
                        UserRepository.AddHistoryRecord("Bid Confirmed - You are the high bidder!", auction.AuctionId);
                        break;
                    }
                    if (items.InnerHtml.Contains(auction.AuctionRef) &&
                        items.InnerHtml.Contains("ERROR: Not an auction"))
                    {
                        confirmed = true;
                        UserRepository.AddHistoryRecord("Bid Failed - The site is no longer an auction",
                            auction.AuctionId);
                        break;
                    }
                }
                if (hdoc.DocumentNode.InnerHtml.Contains("Confirmed Domains") &&
                    hdoc.DocumentNode.InnerHtml.Contains(auction.DomainName)
                    && hdoc.DocumentNode.InnerHtml.Contains("the high bidder"))
                {
                    confirmed = true;
                    UserRepository.AddHistoryRecord("Bid Confirmed - You are the high bidder!", auction.AuctionId);
                }
                if (!confirmed)
                    UserRepository.AddHistoryRecord("Bid Not Confirmed - Data logged", auction.AuctionId);
            }
            else
            {
                UserRepository.AddHistoryRecord(
                    CaptchaOverload
                        ? "Appologies - 3rd party capture solve failure. This has been reported."
                        : "Appologies - Login to account has failed. 3 Seperate attempts made", auction.AuctionId);
            }
        }
    }
}