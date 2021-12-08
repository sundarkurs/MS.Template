# Clean architecture (Onion architecture)

## Domain
- Entities
- Value Objects
- Enumerations
- Enterprise Logic
- Exception

## Applications
- Interfaces
- Models (DTO, ViewModels and etc)
- Application Logic
- Commands
- Queries
- Validators
- Exceptions

## CQRS
- Command Query Responsibility Segregation
- Separate reads (queries) and writes (commands)
- Can maximise performance, scalability, and simplicity
- Easy to add new features, just add a new query or command
- Easy to maintain, changes only affect one command or query

## CQRS + MediatR
- Define commands and queries as requests
- Application layer is just a series of requests/response objects
- Ability to attach additional behaviour before and/or after each request. Example: logging, validation, caching, and etc.
