# GanymedeStation
Garden control system


The garden will contain a series of Pods to monitor conditions.
Pods will include a moisture sensor, thermistor, and radio transmitter.
Pods will be controlled by an Arduino microcontroller.
Each pod will provide the following data:
- id
- moisture
- temperature
- light
- voltage (batter)
This data will be transmitted to a nearby base station.

There will be a base station near the garden to receive radio transmissions from the pods.
Each base station will have a flow meter, water controller, radio receiver, and a connection to the internet.
Data from the pods is aggregated by the base station, and posted to the Ganymede server.

The Ganymede server will receive and store data from the base station(s).
The data may be accessed through a REST API by clients.
POST /[BaseStationId]         - Sets the current battery level and flow value
POST /[BaseStationId]/[PodId] - Sets the current values of the sensors for the pod
GET  /                        - Retrieves a list of metadata for all base stations
GET  /[BaseStationId]         - Retrieves battery level and metadata for all pods associated with the base station
{
    id: string
}
GET  /[BaseStationId]/[PodId] - Retrieves current sensor data for the specified pod

A desktop version of the client will be created in WPF for displaying and controlling each base station.