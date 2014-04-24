#include "Stdafx.h"

using namespace net::openstack::Core::Domain;
using namespace net::openstack::Core::Providers;
using namespace net::openstack::Providers::Rackspace;
using namespace OpenStack::Collections;
using namespace OpenStack::ObjectModel::JsonHome;
using namespace OpenStack::Services::Queues::V1;
using namespace System;
using namespace System::Collections::ObjectModel;
using namespace System::Threading;
using namespace System::Threading::Tasks;

ref class QueueingServiceExamples
{
private:
	static CloudIdentity^ identity = gcnew CloudIdentity();
	static String^ region = nullptr;
	static Guid clientId = Guid::NewGuid();
	static bool internalUrl = false;
	static IIdentityProvider^ identityProvider = nullptr;

public:
	void GetHome()
	{
#pragma region GetHomeAsync (TPL)
		IQueuesService^ queuesService = gcnew CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
		Task<HomeDocument^>^ task = queuesService->GetHomeAsync(CancellationToken::None);
#pragma endregion
	}

	void GetNodeHealth()
	{
#pragma region GetNodeHealthAsync (TPL)
		IQueuesService^ queuesService = gcnew CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
		Task^ task = queuesService->GetNodeHealthAsync(CancellationToken::None);
#pragma endregion
	}

	void CreateQueue()
	{
#pragma region CreateQueueAsync (TPL)
		IQueuesService^ queuesService = gcnew CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
		QueueName^ queueName = gcnew QueueName("ExampleQueue");
		Task<bool>^ task = queuesService->CreateQueueAsync(queueName, CancellationToken::None);
#pragma endregion
	}

	void DeleteQueue()
	{
#pragma region DeleteQueueAsync (TPL)
		IQueuesService^ queuesService = gcnew CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
		QueueName^ queueName = gcnew QueueName("ExampleQueue");
		Task^ task = queuesService->DeleteQueueAsync(queueName, CancellationToken::None);
#pragma endregion
	}

#pragma region ListQueuesAsync (TPL)
	void ListQueues()
	{
		IQueuesService^ queuesService = gcnew CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
		Task<ReadOnlyCollectionPage<CloudQueue^>^>^ queuesPageTask = queuesService->ListQueuesAsync(nullptr, Nullable<int>(), true, CancellationToken::None);
		auto func = gcnew Func<Task<ReadOnlyCollectionPage<CloudQueue^>^>^, Task<ReadOnlyCollection<CloudQueue^>^>^>(GetAllPagesContinuationAsync<CloudQueue^>);
		Task<ReadOnlyCollection<CloudQueue^>^>^ queuesTask = TaskExtensions::Unwrap(queuesPageTask->ContinueWith(func));
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
		IQueuesService^ queuesService = gcnew CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
		QueueName^ queueName = gcnew QueueName("ExampleQueue");
		Task<bool>^ task = queuesService->QueueExistsAsync(queueName, CancellationToken::None);
#pragma endregion
	}
};
