//you may not use this file except in compliance with the License.
//See the NOTICE file distributed with this work for additional
//information regarding copyright ownership.
//You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System.Collections.ObjectModel;

namespace OpenQA.Selenium.Appium.Interfaces
{
    public interface IFindsByIosClassChain<W> : IFindsByFluentSelector<W> where W : IWebElement
    {
        /// <summary>
        /// Finds the first of elements that match the IosClassChain selector supplied
        /// </summary>
        /// <param name="selector">an IosClassChain selector</param>
        /// <returns>IWebElement object so that you can interact that object</returns>
        W FindElementByIosClassChain(string selector);

        /// <summary>
        /// Finds a list of elements that match the IosClassChain selector supplied
        /// </summary>
        /// <param name="selector">an IosClassChain selector</param>
        /// <returns>ReadOnlyCollection of IWebElement objects so that you can interact with those objects</returns>
        ReadOnlyCollection<W> FindElementsByIosClassChain(string selector);
    }
}