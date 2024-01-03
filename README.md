# RedisDemo

The purpose of this application is to understand how Distributed Memory is used in .Net in its simplest form by performing basic operations with Redis. In the project, the In Memory Cache structure and Redis codes are fully coded with comment lines. Likewise, the README is written in an instructive manner.

More comprehensive projects can be developed in line with the information shown here.
<br>
<br>

![redis](https://github.com/zubeyrdamar/RedisDemo/assets/141228392/7b462067-b4ff-4fb4-afd2-4bd3ae365db4)

<hr>

## Installation

* You can download or clone this repository to run the application.
* Afterwards (if you are a Windows or Mac user), you should install and serve a Redis container with the help of Docker.
  - You can take a look at how you can serve Redis with Docker from this link: https://hub.docker.com/_/redis
* Finally, define the host of the Redis you serve in the Program.cs file (or appsetting.json would be a more accurate method).
<hr>

## What is Caching?

We call the process of storing frequently used data in memory caching. There are 2 types of caching.

* ### In Memory Caching

In Memory Caching simply means storing the data to be cached in the server where the application is located.

The most important advantage of this process is that the data can be accessed very quickly. We usually use this method to store data that will be needed again and again for a very short period of time.

However, if we talk about its disadvantages, it is that in cases where more than one of our applications try to access the same data, a copy of the same data must be created for each application. This means that data takes up space in each application, and we may encounter a problem that this data is not the same in all applications.

* ### Distributed Caching

Distributed Caching is the process of storing our data in an external memory. Although this process is slower than In Memory Caching, it is faster than reading data from the Sql database.

Distributed Caching is a caching method we use when many applications need to retrieve data from a single memory or when we want the data to be kept in an area other than our main server.

The most important of these is Redis, which we also mentioned in this application.
<hr>

## What is Redis?

Redis is a NoSql database and works with a key-value structure. One of the most important strengths of Redis is that it provides data consistency.

For more information, read the comment lines in the project.
