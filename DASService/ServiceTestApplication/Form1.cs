using System;
using System.Linq;
using System.Windows.Forms;
using DAS.Domain;
using DAS.Domain.Users;
using GoDaddy;
using Ninject;

namespace ServiceTestApplication
{
    public partial class Form1 : Form
    {
        private readonly IKernel _kernal;
        public Form1(IKernel kernal)
        {
            InitializeComponent();
            _kernal = kernal;
            _kernal.Inject(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            GoDaddyAuctionSniper gd = new GoDaddyAuctionSniper("gilad.fastseo@gmail.com", _kernal.Get<IUserRepository>());
            var loggedin = gd.Login();
            var mo = "";
            //var repo = _kernal.Get<ISystemRepository>();
            ////gd.WinCheck("yolo.com");
            //var auc = repo.GetEndingAuctions().FirstOrDefault();
            //gd.PlaceBid(auc);
            //var resw = gd.GetEndDate("185921902");

            //var res = repo.GetAlerts();
            //var moo = "";
        }
    }
}
