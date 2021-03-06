## Simple Service-Bus
Implement service bus to handle commands and events.To read more about CQRS + ES click [`here`](https://cqrs.nu/Faq) 


## Give a Star! ⭐

If you like or are using this project to learn or using it in your own project, please give it a star. 

Thank you.


## Purpose

The intention of this project is to simple implement concepts of  `service bus`. In this service user is registered. The `UserRegisteredEvent` is then published by the user service and `UserEventHandler` handle the event. Finnaly a welcome message is sent to the user.I use EntityFrameworkCore.InMemory. and try to implement the project by testing.
So, you can easily download and test it. 


## Used Framework and libraries

- **.Net Core 3.1**
- **EntityFrameworkCore.InMemory 5.0.12**
- **xunit 2.4.1**
- **Moq 4.16.1**
- **AutoMapper 8.1.1**



## Concepts

**Command-Bus**

- [`CommandMessage`](https://github.com/YaghoubJalali/SimpleCommandBus/blob/main/src/Simple.ServiceBus/Sample.ServiceBus/Command/CommandMessage.cs) - A type of `command` that would be handled
- [`ICommandHandler<>`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Simple.ServiceBus/Sample.ServiceBus/Contract/CommandBus/ICommandHandler.cs) - The Interface must be implemented to handle each `Command`. Provides `HandleAsync(CommandMessageImplementation)` method.
- [`CommandBus`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Simple.ServiceBus/Sample.ServiceBus/Handler/CommandBus.cs) - Dispatch related `CommandHandler` for each `Command`.



**Event-Bus**

- [`Event`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Simple.ServiceBus/Sample.ServiceBus/Event/Event.cs) - A type of `event` that would be handled
- [`IEventHandler<>`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Simple.ServiceBus/Sample.ServiceBus/Contract/EventBus/IEventHandler.cs) - The Interface must be implemented to handle `Event`. Provides `HandleAsync(EventToHandle)` method.
- [`ActionEventHandler<TEvent>`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Simple.ServiceBus/Sample.ServiceBus/Handler/ActionEventHandler.cs) - The class must be implemented to handle `Event` with specific action.
- [`EventAggregator`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Simple.ServiceBus/Sample.ServiceBus/Handler/EventAggregator.cs) - Subscribe `EventHandler` and `ActionHandler` for `Event`.



**Query-Bus**

- [`IQuery`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Simple.ServiceBus/Sample.ServiceBus/Contract/QueryBus/IQuery.cs) - The Interface containing the filter parameters to apply them on `Query` must be implemented

- [`IResult`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Simple.ServiceBus/Sample.ServiceBus/Contract/QueryBus/IResult.cs) - Type of query result

- [`IQueryHandler<>`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Simple.ServiceBus/Sample.ServiceBus/Contract/QueryBus/IQueryHandler.cs) - The Interface must be implemented to handle each `Query`. Provides `GetQueryAsync(TQueryParameter query)` method.

- [`QueryDispatcher`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Simple.ServiceBus/Sample.ServiceBus/Handler/QueryDispatcher.cs) - Dispatch related `QueryHandler` for each query.

  

## Implementation of Command Bus

#### [`ICommandHandler:`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Simple.ServiceBus/Sample.ServiceBus/Contract/CommandBus/ICommandHandler.cs) Implementation of `ICommandHandler` Interfaces

```
 public interface ICommandHandler { }
    public interface ICommandHandler<in TCommand>
        : ICommandHandler where TCommand : class, ICommandMessage
    {
        Task HandelAsync(TCommand command);
    }
```

#### [`CommandBus:`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Simple.ServiceBus/Sample.ServiceBus/Handler/CommandBus.cs) Implementation of `CommandBus` 

```
public class CommandBus : ICommandBus 
    {
        private readonly IServicesProvider _provier;

        public CommandBus(IServicesProvider provider)
        {
            _provier = provider ?? throw new ArgumentNullException($"{nameof(IServicesProvider)} is null!");
        }

        public async Task DispatchAsync<T>(T command) where T : class, ICommandMessage
        {
           //Validate command

            var handler = _provier.GetService<ICommandHandler<T>>();
            if (handler == null)
                throw new ArgumentNullException(nameof(ICommandHandler<T>));

            await handler.HandelAsync(command);
        }
    }
```

#### [`RegisterUserCommandMessage:`](https://github.com/YaghoubJalali/SimpleCommandBus/blob/main/src/Sample.UserManagement/Sample.UserManagement.Service/Command/UserCommand/RegisterUserCommandMessage.cs) Implement User Message to be handled 

```
public class RegisterUserCommandMessage: CommandMessage
    {
        public RegisterUserCommandMessage(string firstName, string lastName,string email)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
        public string FirstName { get;private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
    }
```

#### [`UserCommandHandler:`](https://github.com/YaghoubJalali/SimpleCommandBus/blob/main/src/Sample.UserManagement/Sample.UserManagement.Service/Command/UserCommand/Handler/UserCommandHandler.cs) Implement handler to handle user command

```
 public class UserCommandHandler : ICommandHandler<RegisterUserCommandMessage>
                                ,ICommandHandler<ModifyUserCommandMessage>
    {
        private readonly IUserRepository _userRepository;
        public UserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task HandelAsync(RegisterUserCommandMessage addUser)
        {
            var user = new UserDbModel
            {
                Id = addUser.Id,
                FirstName = addUser.FirstName,
                LastName = addUser.LastName,
                Email = addUser.Email
            };
            await _userRepository.AddAsync(user);
        }

        public async Task HandelAsync(ModifyUserCommandMessage modifyUser)
        {
            ...
            await _userRepository.UpdateAsync(user);
        }
    }
```



#### [`UserService:`](https://github.com/YaghoubJalali/SimpleCommandBus/blob/main/src/Sample.UserManagement/Sample.UserManagement.Service/Service/UserService.cs)  Dispatch `commandMessage` in user service

```
public class UserService : IUserService
    {
        private readonly ICommandBus _commandBus;

        public UserService(ICommandBus commandBus)
        {
            _commandBus = commandBus ?? throw new ArgumentNullException($"{nameof(ICommandBus)} should not be null!");
        }

        public async Task<Guid> RegisterUser(RegisterUser addUser)
        {
            var addUserCommand = new RegisterUserCommandMessage(addUser.FirstName, addUser.LastName, addUser.Email);
            await _commandBus.DispatchAsync(addUserCommand);
            return addUserCommand.Id;
        }
    }
```



## Implementation of Event Bus

#### [`IEventAggregator:`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Simple.ServiceBus/Sample.ServiceBus/Contract/EventBus/IEventAggregator.cs) Implementation of `IEventAggregator` Interfaces

```
public interface IEventAggregator
    {
        Task Publish<TEvent>(TEvent eventToPublish) where TEvent : IEvent;

        void SubscribeEventHandler<T,U>() 
            where T : IEventHandler<U>
            where U : IEvent;

        void SubscribeActionHandler<T>() where T : IEvent;
    }
```


#### [`EventAggregator:`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Simple.ServiceBus/Sample.ServiceBus/Handler/EventAggregator.cs) Implementation of `EventAggregator` to subscribe eventhandlers and publish event.

```
public class EventAggregator : IEventAggregator
    {
        private readonly IServicesProvider _provier;
        private static List<Type> _subscribersType = new List<Type>();
        public static IEnumerable<Type> SubscriberTypes
        {
            get
            {
                return _subscribersType.AsReadOnly();
            }
        }

        public EventAggregator(IServicesProvider provider)
        {
            _provier = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public void SubscribeEventHandler<T, U>()
            where T : IEventHandler<U>
            where U : IEvent
        {
            _subscribersType.Add(typeof(IEventHandler<U>));
        }

        public void SubscribeActionHandler<T>() where T : IEvent
        {
            _subscribersType.Add(typeof(ActionEventHandler<T>));
        }

        public async Task Publish<T>(T eventToPublish) where T : IEvent
        {
            if(eventToPublish == null)
                throw new ArgumentNullException(nameof(eventToPublish));

            var handlers = GetEventSubscribers(eventToPublish);
            List<Task> actionToHandle = new List<Task>();
            foreach (var handler in handlers)
            {
                var handlerService = GetService<T>(handler);
                if (handlerService == null)
                    throw new ArgumentNullException($"{typeof(IEventHandler<T>)}");

                actionToHandle.Add(handlerService.Handle(eventToPublish));
            }

            await Task.WhenAll(actionToHandle);
        }

        private List<Type> GetEventSubscribers<T>(T eventToPublish) where T : IEvent
        {
            List<Type> types = new List<Type>();
            var handlerTypes = _subscribersType.Where(o => o == typeof(IEventHandler<T>)).ToList();
            types.AddRange(handlerTypes);

            var actionHandlerTypes = _subscribersType.Where(o => o == typeof(ActionEventHandler<T>)).ToList();
            types.AddRange(actionHandlerTypes);

            return types;
        }

        private IEventHandler<T> GetService<T>(Type type) where T : IEvent
        {
            MethodInfo method = _provier.GetType().GetMethod("GetService");
            var genericMethod = method.MakeGenericMethod(type);
            var service = genericMethod.Invoke(_provier, null);
            return service as IEventHandler<T>;
        }
    }
```

#### [`ActionEventHandler:`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Simple.ServiceBus/Sample.ServiceBus/Handler/ActionEventHandler.cs) The Implementation of `ActionEventHandler` class to handle `Event` with specific action

```
public class ActionEventHandler<TEvent> : IEventHandler<TEvent>
                  where TEvent : IEvent
    {
        private Func<TEvent,Task> _actionEvent { get; }
        public ActionEventHandler(Func<TEvent, Task> actionEvent)
        {
            _actionEvent = actionEvent?? throw new ArgumentNullException(nameof(actionEvent));
        }

        public async Task Handle(TEvent eventToHandle)
        {
           await  _actionEvent(eventToHandle);
        }
    }
```

#### [`ActionEventHandlerTest:`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/test/Sample.ServiceBus.Test/Handler/ActionEventHandlerTest.cs) The Implementation of `ActionEventHandlerTest` is to show how to implement and use `ActionEventHandler` class with test

```
public async Task When_InstanciateActionEventHandlerWithAnActionAndPublishEvent_Then_ActionShouldBeCalled()
{
      bool actionMethodCalled = false;
      var userGuid = Guid.NewGuid();
      var testEvent = new TestEvent(userGuid);   
      Task func(TestEvent func)
      {
           //Call any async method here
           actionMethodCalled = true;
           return Task.Delay(1);
       }

       var actionEventHandler = new ActionEventHandler<TestEvent>(func);
       ...

        _actionEventHandlerFixture.EventAggregator.SubscribeActionHandler<TestEvent>();
        await _actionEventHandlerFixture.EventAggregator.Publish(testEvent);

        Assert.True(actionMethodCalled);
}
```

#### [`Event:`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Sample.UserManagement/Sample.UserManagement.Service/Event/UserCreatedEvent.cs) The Implementation of `IEvent` interface and `Event` class

```
public interface IEvent{ }
    
public class Event : IEvent
    {
        public Guid Id { get; private set; }
        public Event(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id));

            Id = id;
        }
    }
```

#### [`UserCreatedEvent:`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Simple.ServiceBus/Sample.ServiceBus/Event/Event.cs) Implementation of `UserCreatedEvent` class. This event is published when the user is created.

```
public class UserCreatedEvent : Event
{
    public UserCreatedEvent(Guid id) : base(id)
    {
    }
}
```

####  [`UserCreatedEventHandler:`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Sample.UserManagement/Sample.UserManagement.Service/Event/Handler/UserCreatedEventHandler.cs)Implementation of `UserCreatedEventHandler` class to handle `UserCreatedEvent`. 


```
public class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
{
    private readonly IEmailService _emailService;
    public UserCreatedEventHandler(IEmailService emailService)
    {
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    }

   public async Task Handle(UserCreatedEvent eventToHandle)
   {
        //validate eventToHandle 
        await _emailService.SendWelcomeMailTo(eventToHandle.Id);
    }
}

```

#### [`UserCommandHandler:`](https://github.com/YaghoubJalali/SimpleCommandBus/blob/main/src/Sample.UserManagement/Sample.UserManagement.Service/Command/UserCommand/Handler/UserCommandHandler.cs) Update `UserCommandhandler`'s HandleAsync method to publish `UserCreatedEvent` after user is created.

```
public UserCommandHandler(IUserRepository userRepository, IEventAggregator eventAggregator)
{
     _userRepository = userRepository;
     _eventAggregator = eventAggregator;
}

public async Task HandelAsync(RegisterUserCommandMessage addUser)
{
     //Create user model
     var user = new UserDbModel{...};
	
     await _userRepository.AddAsync(user);
     
     //Publish UserCreatedEvent after user is added
     await _eventAggregator.Publish(new UserCreatedEvent(user.Id));
}
```

####  [`ConfigurationExtension:`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Sample.UserManagement.Host/Configuration/ConfigurationExtensions.cs) Implementation of static`ConfigurationExtension` class and implement `SubscribeEventHandler` method to register subscribers. 


```
private static void SubscribeEventHandler(IEventAggregator eventAggregator)
{
     eventAggregator.SubscribeEventHandler<UserCreatedEventHandler, UserCreatedEvent>();
} 
```

## Implementation of Query Bus

#### [`GetUserQuery:`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Sample.UserManagement/Sample.UserManagement.Service/Model/GetUserQuery.cs) Implementation of `IQuery` Interfaces containing user filters to apply to user query. 

```
public interface IQuery
{
}

public class GetUserQuery : IQuery
{
     public Guid UserId { get; set; }
}

```


#### [`GetUserQueryResult:`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Sample.UserManagement/Sample.UserManagement.Service/Model/GetUserQueryResult.cs) Implementation of `IResult` Interfaces containing result model properties. 

```
public interface IResult
{
}

public class GetUserQueryResult : IResult
{
    public User  User { get; set; }
}

```

####  [`UserQueryHandler:`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Sample.UserManagement/Sample.UserManagement.Service/Query/UserQueryHandler.cs) Implementation of `IQueryHandler` interface for user query handler. Each query handler must be implement it to handle query filters and return responses.


```
public interface IQueryHandler<in TQueryParameter, TResult>
              where TResult : IResult where TQueryParameter : IQuery
{
      Task<TResult> GetQueryAsync(TQueryParameter query);
}
    
    
public class UserQueryHandler : IQueryHandler<GetUserQuery, GetUserQueryResult>
                                , IQueryHandler<GetUsersQuery, GetUsersQueryResult>
    {
        ...

        public async Task<GetUsersQueryResult> GetQueryAsync(GetUsersQuery query)
        {
            ...
        }

        public async Task<GetUserQueryResult> GetQueryAsync(GetUserQuery query)
        {
            if (query == null || query.UserId == Guid.Empty)
                throw new ArgumentException(nameof(query));

            var dbUser = await _userRepository.GetUserAsync(query.UserId);
            var user = Convertor.ConvertUserDbModelToUser(dbUser);

            return new GetUserQueryResult {  User = user };

        }

        private async Task<User> GetUserAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            var dbUser = await _userRepository.GetUserAsync(userId);
            var user = Convertor.ConvertUserDbModelToUser(dbUser);

            return user;
        }
    }
```

#### [`QueryDispatcher:`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Simple.ServiceBus/Sample.ServiceBus/Handler/QueryDispatcher.cs) Implementation of `QueryDispatcher`.  each method must be call `DispatchAsync` method of `QueryDispatcher` class to dispatch `Query` and get answer. `QueryDispatcher` finds the right handler and sends the query.

```
public interface IQueryDispatcher
{
     Task<TResult> DispatchAsync<TQueryParameter, TResult>(TQueryParameter query) 
         where TQueryParameter : IQuery
         where TResult : IResult;
}
    
public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServicesProvider _provier;

        public QueryDispatcher(IServicesProvider provider)
        {
            _provier = provider ?? throw new ArgumentNullException($"{nameof(IServicesProvider)} is null!");
        }

        public async Task<TResult> DispatchAsync<TQueryParameter, TResult>(TQueryParameter query)
            where TQueryParameter : IQuery
            where TResult : IResult
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var handler = _provier.GetService<IQueryHandler<TQueryParameter, TResult>>();
            if (handler == null)
                throw new ArgumentNullException(nameof(IQueryHandler<TQueryParameter, TResult>));

            return await handler.GetQueryAsync(query);
        }
    }
```

#### [`Dispatcher sample:`](https://github.com/YaghoubJalali/Simple-Service-Bus/blob/main/src/Sample.UserManagement/Sample.UserManagement.Service/Service/UserService.cs) sample use of dispatch method. 

```
public class UserService : IUserService
{
	...
	
  public async Task<User> GetUserAsync(Guid userId)
  {
       if (userId == Guid.Empty)
               throw new ArgumentException(nameof(userId));

        GetUserQuery userFilter = new GetUserQuery { UserId = userId };
        var tempUsers = await _queryDispatcher.DispatchAsync<GetUserQuery, GetUserQueryResult>(userFilter);

        return tempUsers?.User;
   }
}

```


## Roadmap

- [x]  Implement Command Bus
- [x]  Implement Event Bus
- [x]  Implement Query Bus

