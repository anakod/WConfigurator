var myService = '6e400001-b5a3-f393-e0a9-e50e24dcca9e';        // fill in a service you're looking for here
var writeCharacteristic = '6e400002-b5a3-f393-e0a9-e50e24dcca9e';   // fill in a characteristic from the service here
var readCharacteristic = '6e400003-b5a3-f393-e0a9-e50e24dcca9e';

var Device;
var Service;
var Writer;

function connect(){
  navigator.bluetooth.requestDevice({
    // filters: [myFilters]       // you can't use filters and acceptAllDevices together
    filters: [ {services: [ myService ] } ]
  })
  .then(function(device) {
    // save the device returned so you can disconnect later:
    Device = device;
    console.log(device);
    // connect to the device once you find it:
    return device.gatt.connect();
  })
  .then(function(server) {
    // get the primary service:
    return server.getPrimaryService(myService);
  })
  .then(function(service) {
    // get the  characteristic:
	Service = service;
    service.getCharacteristic(readCharacteristic).then(function(read) {
		read.startNotifications()
			.then(subscribeToChanges);
		});
	
	service.getCharacteristic(writeCharacteristic).then(function(write) {
		Writer = write;
		const enc = new TextEncoder();
		write.writeValue(enc.encode('{"t":"ewe","cmd":"abcdef","v":{4650,4350,650,1550,650,1550,700,1550,700,400,700,400,700,400,700,450,650,450,650,1550,700,1500,700,1550,700,400,700,450,650,400,700,450,700,400,700,1500,700,1550,650,1550,700,1500,700,450,700,400,700,400,700,400,700,400,700,450,650,450,700,400,700,1500,700,1550,650,1550,700,1500,700}}\n'))
			.then(() => write.writeValue(Uint8Array.of(0x03)));
	})
  })
  .catch(function(error) {
    // catch any errors:
    console.error('Connection failed!', error);
  });
}

// subscribe to changes from the meter:
function send(request) {
	let cmd = request + '\n\x03';
	const enc = new TextEncoder();
	Writer
		.writeValue(enc.encode(cmd))
		.then(() => write.writeValue(Uint8Array.of(0x03)));
}

// subscribe to changes from the device:
function subscribeToChanges(characteristic) {
  console.log("ready to receive data:");
  characteristic.oncharacteristicvaluechanged = handleData;
}

// handle incoming data:
function handleData(event) {
  // get the data buffer from the meter:
  let decoder = new TextDecoder('utf-8');
  let value = decoder.decode(event.target.value);
  console.log(value);
}

// disconnect function:
function disconnect() {
  if (Device) {
    // disconnect:
    Device.gatt.disconnect();
  }
}
