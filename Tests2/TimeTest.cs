using System.Net.Http;
using TestStack.BDDfy;
using Xunit;

namespace Tests2
{
    public class TimeTest : TimeTestController
    {
        [Fact]
        public void ShouldGetCorrectTime()
        {
            this.Given(x => this.GivenTimeControllerTestContext())
                .And(x => this.GivenRequest(HttpMethod.Get, ""))
                .When(x => this.WhenPerformRequest())
                .Then(x => this.ThenShouldReturnSuccessStatusCode())
                .And(x => this.ThenPersistResult())
                .And(x => this.ParseJsonResponse())
                .And(x => this.ExtractServerTime())
                .And(x => this.GetActualTime())
                .Then(x => this.ThenCompareResults())
                .And(x => this.DifferenceIsLessThen15())
                .BDDfy();
        }
    }
}
