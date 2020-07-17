# Hands-On Parallel Programming with C# 8 and .NET Core 3
Codes of the book: https://www.packtpub.com/application-development/hands-parallel-programming-c-8-and-net-core-3

![Book](https://www.packtpub.com/media/catalog/product/cache/bf3310292d6e1b4ca15aeea773aca35e/9/7/9781789132410-original.png)


# **Section 1: Fundamentals of Threading, Multitasking, and Asynchrony**

## **[Chapter 1: Introduction to Parallel Programming](./Ch1/ "Preparing for multi-core computing")**
* Technical requirements
* Preparing for multi-core computing
* Scenarios where parallel programming can come in handy
* Advantages and disadvantages of parallel programming
* Summary
* Questions

## **Chapter 2: Task Parallelism**
* Technical requirements
* Tasks
* Creating and starting a task
* Getting results from finished tasks
* How to cancel tasks
* How to wait on running tasks
* Handling task exceptions
* Converting APM patterns into tasks
* Converting EAPs into tasks
* More on tasks
* Work-stealing queues
* Summary

## **Chapter 3: Implementing Data Parallelism**
* Technical requirements
* Moving from sequential loops to parallel loops
* Understanding the degree of parallelism
* Creating a custom partitioning strategy
* Canceling loops
* Understanding thread storage in parallel loops
* Summary
* Questions

## **Chapter 4: Using PLINQ**
* Technical requirements
* LINQ providers in .NET
* Writing PLINQ queries
* Preserving order in PLINQ while doing parallel executions
* Merge options in PLINQ
* Throwing and handling exceptions with PLINQ
* Combining parallel and sequential LINQ queries
* Canceling PLINQ queries
* Disadvantages of parallel programming with PLINQ
* Understanding the factors that affect the performance of PLINQ (speedups
* Summary
* Questions

# **Section 2: Data Structures that Support Parallelism in .NET Core**

## **Chapter 5: Synchronization Primitives**
* Technical requirements
* What are synchronization primitives
* Interlocked operations 
* Introduction to locking primitives
* Introduction to signaling primitives
* Lightweight synchronization primitives
* Barrier and countdown events
* SpinWait
* Summary
* Questions

## **Chapter 6: Using Concurrent Collections**
* Technical requirements
* An introduction to concurrent collections
* A multiple producer-consumer scenario
* Summary
* Questions

## **Chapter 7: Improving Performance with Lazy Initialization**
* Technical requirements
* Introducing lazy initialization concepts
* Introducing System.Lazy<T
* Handling exceptions with the lazy initialization pattern
* Lazy initialization with thread-local storage
* Reducing the overhead with lazy initializations
* Summary
* Questions

# **Section 3: Asynchronous Programming Using C#**

## **Chapter 8: Introduction to Asynchronous Programming**
* Technical requirements
* Types of program execution
* When to use asynchronous programming
* When not to use asynchronous programming
* Problems you can solve using asynchronous code
* Summary
* Questions

## **Chapter 9: Async, Await, and Task-Based Asynchronous Programming Basics**
* Technical requirements
* Introducing async and await
* Async delegates and lambda expressions
* Task-based asynchronous patterns
* Exception handling with async code
* Async with PLINQ
* Measuring the performance of async code
* Guidelines for using async code
* Summary
* Questions

# **Section 4: Debugging, Diagnostics, and Unit Testing for Async Code**

## **Chapter 10: Debugging Tasks Using Visual Studio**
* Technical requirements
* Debugging with VS 2019 
* How to debug threads
* Using Parallel Stacks windows
* Using Concurrency Visualizer
* Summary
* Questions
* Further reading 

## **Chapter 11: Writing Unit Test Cases for Parallel and Asynchronous Code**
* Technical requirements
* Unit testing with .NET Core
* Understanding the problems with writing unit test cases for async code
* Writing unit test cases for parallel and async code
* Mocking the setup for async code using Moq
* Testing tools
* Summary
* Questions 
* Further reading

# **Section 5: Parallel Programming Feature Additions to .NET Core**

## **Chapter 12: IIS and Kestrel in ASP.NET Core**
* Technical requirements
* IIS threading model and internals
* Kestrel threading model and internals
* Introducing the best practices of threading in microservices
* Introducing async in ASP.NET MVC core
* Summary
* Questions

## **Chapter 13: Patterns in Parallel Programming**
* Technical requirements
* The MapReduce pattern
* Aggregation
* The fork/join pattern
* The speculative processing pattern
* The lazy pattern
* Shared state pattern
* Summary
* Questions

## **Chapter 14: Distributed Memory Management**
* Technical requirements
* Introduction to distributed systems
* Shared versus distributed memory model
* Types of communication network
* Properties of communication networks
* Exploring topologies
* Programming distributed memory machines using message passing
* Collectives
* Summary
* Questions