# Whmcs.Net

Whmcs API Wrapper, made for .Net

A quick fork of this project to explain separation of concerns and mocking. Dependency injection is also required but has not been implemented.

One method of WhmcsApi.cs has been refactored (GetProductsByProductId), resulting in the following new classes:

NewApi.cs - A replacement for WhmcsApi.cs to isolate the new class. Has IDataBroker constructor injected.

APIDataBroker.cs - Responsible for taking requests and passing them to the APIService, and de-serialising the returned JSON. Has IJSONService and IAPIService constructor injected.

JSONService.cs - A wrapper for JsonConvert.DeserializeObject<T> in order to be able to test classes that use JsonConvert.

APIService.cs - The class responsible for actually calling the external API. All references to credentials, domains etc are now isolated within this class, in this quick implementation they are populated via calling the InitialiseAPI method before making any calls to the data broker.

New tests:
TestNewApi.cs
TestAPIDataBroker.cs
No tests for APIService.cs currently - these would be integration tests and require access to a real API server (which I don't have).
