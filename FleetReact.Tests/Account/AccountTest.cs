using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using StructureMap;
using Fleet.Data;
using Fleet.Model;

namespace Fleet.Tests
{
    [TestClass]
    public class AccountTest : BaseServiceTest
    {
        public int ACCOUNT_ID = 4317; //4344 - ofir // 4319 -n
        public int USER_ID = 3903;
        public int USER_TYPE = 2;
        string USER_NAME = "A";

        public AccountTest()
        {
            SetCurrentUser(USER_NAME, 1, false);

        }
        private void SetCurrentUser(string userName, int langId, bool isSso)
        {
            ServiceFunctionHelper.SetHttpContext(userName, langId, isSso);
            USER_ID = HttpContext.Current.User.GetCurrentUserId().Value;
            USER_TYPE = HttpContext.Current.User.GetCurrentUserType().Value;
            ACCOUNT_ID = HttpContext.Current.User.GetCurrentAccountId().Value;

        }
        [TestMethod]
        public void TestMethod1()
        {
            IAccountRepository _repository = ObjectFactory.Container.GetInstance<IAccountRepository>();
            BaseResultInfo res = _repository.GetAccountDetails(ACCOUNT_ID);
            Assert.IsNull(res.Error);
        }
    }
}
