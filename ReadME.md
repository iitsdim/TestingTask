# Digital Traffic Light, Testing Task

#### Run the App:
first in directory DigitalTrafficLight, create DB
```
docker compose up -d
```
Then apply all the migrations
```
dotnet ef --project Domain database update
```
Then you free to start the project with
```
dotnet run --project Domain
```


#### Api:
APIs list with example responses
##### Create Sequence 
API path which creates new sequence
```
POST {{base_url}}/sequence/create
```
Example Response
```
{
    "sequence": "addc8351-b648-49f9-ab2d-8561c56ff95a"
}
```

##### Add Observation
API path which add new Observation and Response with Digital Traffic Light state
```
POST {{base_url}}/observation/add
```
start: returns array of possible initial numbers by which observation might have started
missing: returns array of pair of binary string, where 1 means this definetily broken, 0 means might be not broken
```
{
    "start": [],
    "missing": []
}
```

##### Clear
API path which for deletion of all the DB
```
GET {{base_url}}/clear
```
returns ok if succed
```
{
    "response": "ok"
}
```
#### Docker containers
To check DB you can enter inside psql container in docker
```
docker exec -it 8d2c4b16c576 bash 
psql postgres -U myuser
```