## Sample Applications built on Microsoft Orleans actor system.

### 1. Template App
A minimal template solution which includes projects for Grains interfaces, Grains implementations, as well as a pre-configured Host and Client projects (console applications). Cluster is configured with local hosting.

### 2. Distributed Cache
A distributed key-value cache implementation where each entry in the cache is modeled as a grain in Orleans. Orleans' grain persistence and activations make it a good fit for a distribured cache system.

### 3. Weather Service App
A weather service application which lets users query the weather in a city. Each city is modeled as a grain in the system. Once activated, cities will refresh their own weather data periodically. Uses AccuWeather API to fetch weather data. To access AccuWeather API you will need an API key, a free version of which can be retrieved through their developer's program. The client app is a basic console application since a fancy UI is out of scope for this sample application.

All of the projects target .Net Core 2.1 and can run on Linux, Mac and Windows.
