## Simple Service-Bus
Implement full test service bus to handle commands.



## Give a Star! ⭐

If you like or are using this project to learn or using it in your own project, please give it a star. 

Thank you.



## Purpose

The intention of this project is to implement concepts of different kinds of `service bus`.



## Used Framework and libraries

- **.Net Core 3.1**
- **EntityFrameworkCore.InMemory 5.0.12**
- **xunit 2.4.1**
- **Moq 4.16.1**
- **AutoMapper 8.1.1**



## Concepts

**Command-Bus**

- [`CommandMessage`](https://github.com/YaghoubJalali/SimpleCommandBus/blob/main/src/Simple.ServiceBus/Sample.ServiceBus/Command/CommandMessage.cs) - A type of `command` that would be handled
- [`ICommandHandler<>`](https://github.com/YaghoubJalali/SimpleCommandBus/blob/main/src/Simple.ServiceBus/Sample.ServiceBus/Contract/ICommandHandler.cs) - The Interface must be implemented to handle each `Command`. Provides `handleAsync(CommandMessageImplementation)` Method.
- [`CommandBus`](https://github.com/YaghoubJalali/SimpleCommandBus/blob/main/src/Simple.ServiceBus/Sample.ServiceBus/Handler/CommandBus.cs) - Dispatch related `CommandHandler` for each `Command`.

## 

## Example Implementation of Command Bus:

#### [`ICommandHandler:`](https://github.com/YaghoubJalali/SimpleCommandBus/blob/main/src/Simple.ServiceBus/Sample.ServiceBus/Contract/ICommandHandler.cs) The only Implementation of `ICommandHandler` Interfaces

```
 public interface ICommandHandler { }
    public interface ICommandHandler<in TCommand>
        : ICommandHandler where TCommand : class, ICommandMessage
    {
        Task HandelAsync(TCommand command);
    }
```



#### [`CommandBus:`](https://github.com/YaghoubJalali/SimpleCommandBus/blob/main/src/Simple.ServiceBus/Sample.ServiceBus/Handler/CommandBus.cs) The Only Implementation of `CommandBus` 

```
public class CommandBus : ICommandBus 
    {
        private readonly IServicesProvider _provier;

        public CommandBus(IServicesProvider provider)
        {
            _provier = provider ?? throw new ArgumentNullException($"{nameof(IServicesProvider)} is null!");
        }

        public async Task Dispatch<T>(T command) where T : class, ICommandMessage
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

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

#### 

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
            var user = new UserDbModel
            {
                Id = modifyUser.Id,
                FirstName = modifyUser.FirstName,
                LastName = modifyUser.LastName,
                Email = modifyUser.Email
            };
            await _userRepository.UpdateAsync(user);
        }
    }
```



#### [`UserService:`](https://github.com/YaghoubJalali/SimpleCommandBus/blob/main/src/Sample.UserManagement/Sample.UserManagement.Service/Service/UserService.cs) - Dispatch `commandMessage` in user service

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
            await _commandBus.Dispatch(addUserCommand);
            return addUserCommand.Id;
        }
    }
```




## Roadmap

- [x]  Implement Command Bus

- [ ]  Implement Event Bus
- [ ]  Implement Query Bus

