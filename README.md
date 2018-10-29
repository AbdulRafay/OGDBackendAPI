# OGDBackendAPI

# Projects:
  - OGD Assignment (Web Proj)
  - MovieSearch (Lib Proj)
  - APIClient (Lib Proj)

# Requirements:
  - Local installation of Redis server at 127.0.0.1
  - If you want to run without cache please set cache:false in searchsettings.json in MovieSearch project
    - comment out following Redis config in MovieSearch\StartupExtensions.cs
    ~~~~ 
            services.AddDistributedRedisCache(option =>
                {
                  option.Configuration = "127.0.0.1";                
                });
  
# Build with visual studio

  - Please make sure the project runs on port 44387 (IIS Express) in order for it to communicate with Front end application (https://github.com/AbdulRafay/MovieApp)
  
  

