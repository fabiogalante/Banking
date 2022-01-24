# Banking - Transfer

Testing - https://github.com/acessocard/test-backend

### Installing
Follow these steps to get your development environment set up: (Before Run Start the Docker Desktop)
1. Clone the repository
2. Once Docker for Windows is installed, go to the **Settings > Advanced option**, from the Docker icon in the system tray, to configure the minimum amount of memory and CPU like so:
* **Memory: 4 GB**
* CPU: 2
3. At the root directory which include **docker-compose.yml** files, run below command:
```csharp
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```
3. Wait for docker compose all microservices. That’s it! (some microservices need extra time to work so please wait if not worked in first shut)

4. You can **launch microservices** as below urls:

* **Transfer API -> http://host.docker.internal:8002/swagger/index.html**
* **Processing API -> http://host.docker.internal:8005/swagger/index.html**

![Project](https://user-images.githubusercontent.com/2528800/150765300-23285e64-d26c-4e2b-b12e-c2e2ebed44c7.png)


## License
[MIT](https://choosealicense.com/licenses/mit/)
