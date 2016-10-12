using System;
using System.Windows.Forms;
using DAS.Domain.GoDaddy.Users;
using DAS.Domain.Users;
using DAS.GoDaddyv2;
using Ninject;

namespace Test
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

        private void button1_Click(object sender, EventArgs e)
        {
            var repo = _kernal.Get<IGoDaddySession>();
            GoDaddyAuctionSniper gd = new GoDaddyAuctionSniper("michaelgipmedia", _kernal.Get<IUserRepository>());
            //var loggedin = gd.Login(0, repo.Username, repo.Password);
            //var mo = gd.WatchList();
            //var repo = _kernal.Get<ISystemRepository>();
            var blah = gd.Search("cat");
            var moo = "";
            
            //gd.WinCheck("yolo.com");
        }
    }
}
