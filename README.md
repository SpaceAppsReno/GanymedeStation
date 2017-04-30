# GanymedeStation
Garden control system

<p>
The garden will contain a series of Pods to monitor conditions.
Pods will include a moisture sensor, thermistor, and radio transmitter.
Pods will be controlled by an Arduino microcontroller.
</p>

Each pod will provide the following data:
* id
* moisture
* temperature
* light
* voltage (batter)
This data will be transmitted to a nearby base station.

<p>
There will be a base station near the garden to receive radio transmissions from the pods.
Each base station will have a flow meter, water controller, radio receiver, and a connection to the internet.
Data from the pods is aggregated by the base station, and posted to the Ganymede server.
</p>

<p>
The Ganymede server will receive and store data from the base station(s).
The data may be accessed through a REST API by clients.
</p>
<p>PUT /api/[BaseStationId]         - Sets the current battery level and flow value</p>
    <p>
    {
      "name":"MyBaseStation",
      "flow":"9000",
      "voltage":"3.3",
      "valves":["valve1","valve2"]
    }
    </p>
<p>PUT /[BaseStationId]/[PodId] - Sets the current values of the sensors for the pod</p>
    <p>
    {
      "name":"MyPod1",
      "temperature":"16",
      "moisture":"255",
      "light":"50",
      "voltage":"3.3"
    }
    </p>
<p>GET  /                        - Retrieves a list of metadata for all base stations</p>
    <p>
    [
      {"name":"MyBaseStation"},
      {"name":"MyBaseStation2"}
    ]
    </p>
<p>GET  /[BaseStationId]         - Retrieves battery level and metadata for all pods associated with the base station</p>
    <p>
    {
      "_id":"59061e3e8f9ad902fdac9d17",
      "modified":"2017-04-30T17:26:22.255Z",
      "name":"MyBaseStation",
      "flow":9000,
      "voltage":3.3,
      "created":"2017-04-30T17:26:22.254Z",
      "__v":1,
      "valves":["valve1","valve2"],
      "pods":["MyPod1"]
    }
    </p>
<p>GET  /[BaseStationId]/[PodId] - Retrieves current sensor data for the specified pod</p>
    <p>
    {
     "_id":"59061e568f9ad902fdac9d18",
     "light":50,
     "moisture":255,
     "temperature":16,
     "voltage":3.3,
     "name":"MyPod1",
     "baseStation":"MyBaseStation",
     "created":"2017-04-30T17:26:46.731Z",
     "__v":0
    }
    </p>
<p>
A desktop version of the client will be created in WPF for displaying and controlling each base station.
</p>
