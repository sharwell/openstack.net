#include "Stdafx.h"

using namespace OpenStack::Collections;
using namespace OpenStack::ObjectModel::JsonHome;
using namespace OpenStack::Security::Authentication;
using namespace OpenStack::Services::Queues::V1;
using namespace Rackspace::Threading;
using namespace System;
using namespace System::Collections::ObjectModel;
using namespace System::Net::Http;
using namespace System::Threading;
using namespace System::Threading::Tasks;
using namespace Rackspace::Threading;

ref class QueueingServiceExamples
{
private:
	static String^ region = nullptr;
	static Guid clientId = Guid::NewGuid();
	static bool internalUrl = false;
	static IAuthenticationService^ authenticationService = nullptr;

#pragma region GetHomeAsync
#pragma region PrepareGetHomeAsync (TPL)
private:
	ref class AcquirePrepareGetHomeAsync sealed
	{
		IQueuesService^ service;
		CancellationToken cancellationToken;

	public:
		AcquirePrepareGetHomeAsync(IQueuesService^ service, CancellationToken cancellationToken)
			: service(service)
			, cancellationToken(cancellationToken)
		{
		}

		Task<GetHomeApiCall^>^ Invoke()
		{
			return service->PrepareGetHomeAsync(cancellationToken);
		}
	};

	ref class BodyPrepareGetHomeAsync sealed
	{
		CancellationToken cancellationToken;

	public:
		BodyPrepareGetHomeAsync(CancellationToken cancellationToken)
			: cancellationToken(cancellationToken)
		{
		}

		Task<Tuple<HttpResponseMessage^, HomeDocument^>^>^ Invoke(Task<GetHomeApiCall^>^ task)
		{
			return task->Result->SendAsync(cancellationToken);
		}
	};

	static HomeDocument^ SelectGetHomeResult(Task<Tuple<HttpResponseMessage^, HomeDocument^>^>^ task)
	{
		return task->Result->Item2;
	}

public:
	void PrepareGetHome()
	{
		IQueuesService^ queuesService = gcnew QueuesClient(authenticationService, region, clientId, internalUrl);
		auto acquireWrapper = gcnew AcquirePrepareGetHomeAsync(queuesService, CancellationToken::None);
		auto acquire = gcnew Func<Task<GetHomeApiCall^>^>(acquireWrapper, &AcquirePrepareGetHomeAsync::Invoke);
		auto bodyWrapper = gcnew BodyPrepareGetHomeAsync(CancellationToken::None);
		auto body = gcnew Func<Task<GetHomeApiCall^>^, Task<Tuple<HttpResponseMessage^, HomeDocument^>^>^>(bodyWrapper, &BodyPrepareGetHomeAsync::Invoke);
		auto select = gcnew Func<Task<Tuple<HttpResponseMessage^, HomeDocument^>^>^, HomeDocument^>(&SelectGetHomeResult);
		Task<HomeDocument^>^ task = CoreTaskExtensions::Select(CoreTaskExtensions::Using(acquire, body), select);
	}
#pragma endregion

	void GetHome()
	{
#pragma region GetHomeAsync (TPL)
		IQueuesService^ queuesService = gcnew QueuesClient(authenticationService, region, clientId, internalUrl);
		Task<HomeDocument^>^ task = QueuesServiceExtensions::GetHomeAsync(queuesService, CancellationToken::None);
#pragma endregion
	}
#pragma endregion

#pragma region GetNodeHealthAsync
#pragma region PrepareGetNodeHealthAsync (TPL)
private:
	ref class AcquirePrepareGetNodeHealthAsync sealed
	{
		IQueuesService^ service;
		CancellationToken cancellationToken;

	public:
		AcquirePrepareGetNodeHealthAsync(IQueuesService^ service, CancellationToken cancellationToken)
			: service(service)
			, cancellationToken(cancellationToken)
		{
		}

		Task<GetNodeHealthApiCall^>^ Invoke()
		{
			return service->PrepareGetNodeHealthAsync(cancellationToken);
		}
	};

	ref class BodyPrepareGetNodeHealthAsync sealed
	{
		CancellationToken cancellationToken;

	public:
		BodyPrepareGetNodeHealthAsync(CancellationToken cancellationToken)
			: cancellationToken(cancellationToken)
		{
		}

		Task<Tuple<HttpResponseMessage^, bool>^>^ Invoke(Task<GetNodeHealthApiCall^>^ task)
		{
			return task->Result->SendAsync(cancellationToken);
		}
	};

	static bool SelectGetNodeHealthResult(Task<Tuple<HttpResponseMessage^, bool>^>^ task)
	{
		return task->Result->Item2;
	}

public:
	void PrepareGetNodeHealth()
	{
		IQueuesService^ queuesService = gcnew QueuesClient(authenticationService, region, clientId, internalUrl);
		auto acquireWrapper = gcnew AcquirePrepareGetNodeHealthAsync(queuesService, CancellationToken::None);
		auto acquire = gcnew Func<Task<GetNodeHealthApiCall^>^>(acquireWrapper, &AcquirePrepareGetNodeHealthAsync::Invoke);
		auto bodyWrapper = gcnew BodyPrepareGetNodeHealthAsync(CancellationToken::None);
		auto body = gcnew Func<Task<GetNodeHealthApiCall^>^, Task<Tuple<HttpResponseMessage^, bool>^>^>(bodyWrapper, &BodyPrepareGetNodeHealthAsync::Invoke);
		auto select = gcnew Func<Task<Tuple<HttpResponseMessage^, bool>^>^, bool>(&SelectGetNodeHealthResult);
		Task<bool>^ task = CoreTaskExtensions::Select(CoreTaskExtensions::Using(acquire, body), select);
	}
#pragma endregion

	void GetNodeHealth()
	{
#pragma region GetNodeHealthAsync (TPL)
		IQueuesService^ queuesService = gcnew QueuesClient(authenticationService, region, clientId, internalUrl);
		Task<bool>^ operationalTask = QueuesServiceExtensions::GetNodeHealthAsync(queuesService, CancellationToken::None);
#pragma endregion
	}
#pragma endregion

#pragma region CreateQueueAsync
#pragma region PrepareCreateQueueAsync (TPL)
private:
	ref class AcquirePrepareCreateQueueAsync sealed
	{
		IQueuesService^ service;
		QueueName^ queueName;
		CancellationToken cancellationToken;

	public:
		AcquirePrepareCreateQueueAsync(IQueuesService^ service, QueueName^ queueName, CancellationToken cancellationToken)
			: service(service)
			, queueName(queueName)
			, cancellationToken(cancellationToken)
		{
		}

		Task<CreateQueueApiCall^>^ Invoke()
		{
			return service->PrepareCreateQueueAsync(queueName, cancellationToken);
		}
	};

	ref class BodyPrepareCreateQueueAsync sealed
	{
		CancellationToken cancellationToken;

	public:
		BodyPrepareCreateQueueAsync(CancellationToken cancellationToken)
			: cancellationToken(cancellationToken)
		{
		}

		Task<Tuple<HttpResponseMessage^, bool>^>^ Invoke(Task<CreateQueueApiCall^>^ task)
		{
			return task->Result->SendAsync(cancellationToken);
		}
	};

	static bool SelectCreateQueueResult(Task<Tuple<HttpResponseMessage^, bool>^>^ task)
	{
		return task->Result->Item2;
	}

public:
	void PrepareCreateQueue()
	{
		IQueuesService^ queuesService = gcnew QueuesClient(authenticationService, region, clientId, internalUrl);
		QueueName^ queueName = gcnew QueueName("ExampleQueue");
		auto acquireWrapper = gcnew AcquirePrepareCreateQueueAsync(queuesService, queueName, CancellationToken::None);
		auto acquire = gcnew Func<Task<CreateQueueApiCall^>^>(acquireWrapper, &AcquirePrepareCreateQueueAsync::Invoke);
		auto bodyWrapper = gcnew BodyPrepareCreateQueueAsync(CancellationToken::None);
		auto body = gcnew Func<Task<CreateQueueApiCall^>^, Task<Tuple<HttpResponseMessage^, bool>^>^>(bodyWrapper, &BodyPrepareCreateQueueAsync::Invoke);
		auto select = gcnew Func<Task<Tuple<HttpResponseMessage^, bool>^>^, bool>(&SelectCreateQueueResult);
		Task<bool>^ task = CoreTaskExtensions::Select(CoreTaskExtensions::Using(acquire, body), select);
	}
#pragma endregion

	void CreateQueue()
	{
#pragma region CreateQueueAsync (TPL)
		IQueuesService^ queuesService = gcnew QueuesClient(authenticationService, region, clientId, internalUrl);
		QueueName^ queueName = gcnew QueueName("ExampleQueue");
		Task<bool>^ task = QueuesServiceExtensions::CreateQueueAsync(queuesService, queueName, CancellationToken::None);
#pragma endregion
	}
#pragma endregion

#pragma region RemoveQueueAsync
#pragma region PrepareRemoveQueueAsync (TPL)
private:
	ref class AcquirePrepareRemoveQueueAsync sealed
	{
		IQueuesService^ service;
		QueueName^ queueName;
		CancellationToken cancellationToken;

	public:
		AcquirePrepareRemoveQueueAsync(IQueuesService^ service, QueueName^ queueName, CancellationToken cancellationToken)
			: service(service)
			, queueName(queueName)
			, cancellationToken(cancellationToken)
		{
		}

		Task<RemoveQueueApiCall^>^ Invoke()
		{
			return service->PrepareRemoveQueueAsync(queueName, cancellationToken);
		}
	};

	ref class BodyPrepareRemoveQueueAsync sealed
	{
		CancellationToken cancellationToken;

	public:
		BodyPrepareRemoveQueueAsync(CancellationToken cancellationToken)
			: cancellationToken(cancellationToken)
		{
		}

		Task<Tuple<HttpResponseMessage^, String^>^>^ Invoke(Task<RemoveQueueApiCall^>^ task)
		{
			return task->Result->SendAsync(cancellationToken);
		}
	};

public:
	void PrepareRemoveQueue()
	{
		IQueuesService^ queuesService = gcnew QueuesClient(authenticationService, region, clientId, internalUrl);
		QueueName^ queueName = gcnew QueueName("ExampleQueue");
		auto acquireWrapper = gcnew AcquirePrepareRemoveQueueAsync(queuesService, queueName, CancellationToken::None);
		auto acquire = gcnew Func<Task<RemoveQueueApiCall^>^>(acquireWrapper, &AcquirePrepareRemoveQueueAsync::Invoke);
		auto bodyWrapper = gcnew BodyPrepareRemoveQueueAsync(CancellationToken::None);
		auto body = gcnew Func<Task<RemoveQueueApiCall^>^, Task<Tuple<HttpResponseMessage^, String^>^>^>(bodyWrapper, &BodyPrepareRemoveQueueAsync::Invoke);
		Task^ removeTask = CoreTaskExtensions::Using(acquire, body);
	}
#pragma endregion

	void RemoveQueue()
	{
#pragma region RemoveQueueAsync (TPL)
		IQueuesService^ queuesService = gcnew QueuesClient(authenticationService, region, clientId, internalUrl);
		QueueName^ queueName = gcnew QueueName("ExampleQueue");
		Task^ task = QueuesServiceExtensions::RemoveQueueAsync(queuesService, queueName, CancellationToken::None);
#pragma endregion
	}
#pragma endregion

#pragma region ListQueuesAsync (TPL)
	void ListQueues()
	{
		IQueuesService^ queuesService = gcnew QueuesClient(authenticationService, region, clientId, internalUrl);
		Task<ReadOnlyCollectionPage<Queue^>^>^ queuesPageTask = queuesService->ListQueuesAsync(nullptr, Nullable<int>(), true, CancellationToken::None);
		auto func = gcnew Func<Task<ReadOnlyCollectionPage<Queue^>^>^, Task<ReadOnlyCollection<Queue^>^>^>(GetAllPagesContinuationAsync<Queue^>);
		Task<ReadOnlyCollection<Queue^>^>^ queuesTask = TaskExtensions::Unwrap(queuesPageTask->ContinueWith(func));
	}

	generic<class T>
	static Task<ReadOnlyCollection<T>^>^ GetAllPagesContinuationAsync(Task<ReadOnlyCollectionPage<T>^>^ pageTask)
	{
		return ReadOnlyCollectionPageExtensions::GetAllPagesAsync(pageTask->Result, CancellationToken::None, static_cast<IProgress<ReadOnlyCollectionPage<T>^>^>(nullptr));
	}
#pragma endregion

	void QueueExists()
	{
#pragma region QueueExistsAsync (TPL)
		IQueuesService^ queuesService = gcnew QueuesClient(authenticationService, region, clientId, internalUrl);
		QueueName^ queueName = gcnew QueueName("ExampleQueue");
		Task<bool>^ task = queuesService->QueueExistsAsync(queueName, CancellationToken::None);
#pragma endregion
	}
};
