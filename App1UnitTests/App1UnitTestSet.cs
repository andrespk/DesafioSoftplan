using App1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App1UnitTests
{
    [TestClass]
    public class App1UnitTestSet
    {
        [TestMethod]
        public void Teste_RecuperarTaxaJuros()
        {
            var controller = new InterestsController();
            var rate = controller.GetRate() as OkObjectResult;
            Assert.AreEqual(rate.Value, 0.01);
        }

        [TestMethod]
        public void Teste_RecuperarShowMeTheCode()
        {
            var controller = new InterestsController();
            var message = controller.ShowMeTheCode() as OkObjectResult;
            Assert.IsNotNull(message);
            Assert.IsTrue(message.Value.ToString().Contains("andrespk", System.StringComparison.InvariantCultureIgnoreCase));
        }

        [TestMethod]
        public void Teste_CalcularTaxaJurosValorCorreto()
        {
            var controller = new InterestsController();
            var interests = controller.Calc(new App1.Models.InterestsInputs
            {
                valorInicial = 100,
                meses = 5
            }) as OkObjectResult;
            Assert.IsNotNull(interests);
            Assert.AreEqual(interests.Value, 105.1);
        }

        [TestMethod]
        public void Teste_CalcularTaxaJurosValorIncorreto()
        {
            var controller = new InterestsController();
            var interests = controller.Calc(new App1.Models.InterestsInputs
            {
                valorInicial = -100,
                meses = 5
            });
            Assert.IsNotNull(interests);
            Assert.AreEqual(interests.GetType(), typeof(BadRequestResult));
        }
    }
}