# Ganymede Agent Guidelines

This document provides authoritative rules and instructions for LLM agents working with the Ganymede codebase. These guidelines ensure consistency, maintainability, and adherence to the project's architecture and patterns.

## Project Overview

Ganymede is a .NET framework for building cross-platform UI applications with MVVM patterns. It provides:
- Navigation services for ViewModel-based navigation
- Dialog services for user interactions
- Base ViewModel classes and helpers
- Platform-specific implementations (Avalonia, WPF)

## Core Architecture Principles

### 1. MVVM Pattern Compliance
- All ViewModels must implement `IViewModel` or derived interfaces
- Commands should be created using `CommandBuilder` helper
- ViewModels should be stateless and focused on business logic
- Bindings should be handled through the platform-specific implementations

### 2. Interface Segregation
- Services are defined through interfaces (e.g., `INavigationService`, `IDialogService`)
- Implementations are provided per platform
- Dependencies should be injected through interfaces, not concrete types

### 3. Navigation Patterns
- Navigation is handled through `INavigationService`
- Use `Navigate<TViewModel>()` for new instances
- Use `NavigateAndReset<TViewModel>()` for complete navigation resets
- Navigation sets are managed automatically with stack-based navigation

### 4. Dialog Services
- Dialog services provide standardized user interaction patterns
- Dialog ViewModels should implement `IDialogViewModel`
- Use `DialogService` to show messages, inputs, and custom dialogs

## Code Style and Conventions

### 1. Naming Conventions
- Use PascalCase for all public members and types
- Use camelCase for private fields and local variables
- ViewModel classes should end with "ViewModel"
- View classes should end with "View"
- Service classes should end with "Service"
- Interface names should start with "I"

### 2. Documentation Standards
- All public APIs must have XML documentation comments
- Include summary, parameters, and return value documentation
- Use consistent terminology throughout the codebase
- Document complex logic with inline comments

### 3. Code Structure
- Keep classes focused and single-responsibility
- Group related functionality together
- Use meaningful names for methods and properties
- Avoid deeply nested code structures

## Implementation Guidelines

### 1. ViewModel Creation
```csharp
public class MyViewModel : ViewModel
{
    public ICommand MyCommand { get; }
    
    public MyViewModel()
    {
        var cb = CommandBuilder.For(this);
        MyCommand = cb.BuildSimple(OnMyAction);
    }
    
    private async Task OnMyAction()
    {
        // Implementation
    }
}
```

### 2. Navigation
```csharp
// Navigate to a new ViewModel instance
await NavigationService.Navigate<MyViewModel>();

// Navigate with state
await NavigationService.Navigate<MyViewModel, string>("Hello World");

// Navigate and reset the navigation stack
await NavigationService.NavigateAndReset<MyViewModel>();
```

### 3. Dialog Usage
```csharp
// Show a message dialog
await DialogService.Message("Title", "Message");

// Show an input dialog
var result = await DialogService.Input("Enter value", "Prompt");

// Show a custom dialog
await DialogService.Show<MyDialogViewModel>();
```

## Platform-Specific Considerations

### Avalonia Implementation
- Uses Avalonia-specific controls and services
- Implements `AvaloniaConventionVisualResolver`
- Uses `NavigationHost` for navigation containers

### WPF Implementation
- Uses WPF-specific controls and services
- Implements WPF-specific visual resolvers
- Uses `NavigationHost` for navigation containers

## Testing Guidelines

### 1. Test Framework
- Write unit tests using NUnit4 syntax
- Use `Assert.That` styling for assertions
- Use *Moq* for mocking interfaces and dependencies where required

### 2. Unit Testing
- Test ViewModels in isolation
- Mock dependencies using interfaces
- Test command execution and state changes
- Verify navigation and dialog service interactions

### 3. Integration Testing
- Test platform-specific implementations
- Verify navigation stack behavior
- Test dialog service functionality
- Validate visual resolution patterns

## Best Practices

### 1. Error Handling
- Use try-catch blocks for async operations
- Implement proper cancellation handling
- Log errors appropriately
- Provide user-friendly error messages

### 2. Performance
- Avoid unnecessary object creation
- Use `async/await` appropriately
- Implement proper disposal patterns
- Minimize UI thread blocking

### 3. Maintainability
- Keep code clean and readable
- Follow SOLID principles
- Avoid code duplication
- Update documentation when code changes

## Change Management

### 1. Pull Request Requirements
- All changes must include appropriate tests
- Documentation must be updated
- Code must follow existing patterns
- Changes must not break existing functionality

### 2. Code Review Process
- All PRs must be reviewed by at least one maintainer
- Code must follow established conventions
- Performance implications must be considered
- Security considerations must be addressed

## Versioning and Release

### 1. Semantic Versioning
- Major version changes for breaking API changes
- Minor version changes for new features
- Patch version changes for bug fixes

### 2. Release Process
- All releases must pass CI/CD pipeline
- Documentation must be updated for each release
- Release notes must be comprehensive
- Package must be published to NuGet

## Contributing Guidelines

### 1. Getting Started
- Review the README for project setup
- Understand the architecture patterns
- Run existing tests to verify setup
- Follow the code style and conventions

### 2. Development Workflow
- Create feature branches from main
- Follow commit message conventions
- Write clear, descriptive commit messages
- Keep commits focused and atomic

### 3. Communication
- Use GitHub issues for tracking work
- Discuss major changes in issues or PRs
- Follow project communication guidelines
- Be respectful and constructive in all interactions

## Security Considerations

### 1. Input Validation
- Validate all user inputs
- Sanitize data before processing
- Implement proper authorization checks
- Protect against injection attacks

### 2. Data Protection
- Handle sensitive data appropriately
- Use secure communication channels
- Implement proper access controls
- Follow data privacy regulations

## Compatibility Requirements

### 1. .NET Compatibility
- Target .NET 8.0 as minimum version
- Ensure compatibility with platform-specific frameworks
- Test across supported platforms

### 2. Backward Compatibility
- Maintain API compatibility when possible
- Provide migration paths for breaking changes
- Document breaking changes clearly
- Consider deprecation warnings for deprecated features

## Troubleshooting

### 1. Common Issues
- Navigation stack issues: Verify navigation service usage
- Dialog service problems: Check dialog service registration
- Platform-specific issues: Confirm platform implementation
- Build failures: Ensure all dependencies are correctly referenced

### 2. Debugging
- Use logging to trace execution flow
- Inspect navigation stacks for issues
- Verify service registrations
- Check ViewModel state during execution

This document serves as the authoritative reference for all agents working with the Ganymede codebase. Any changes to these guidelines must be approved by the project maintainers.