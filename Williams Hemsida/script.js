let tabledata = document.getElementById('tabledata')
var myDate
fetch('http://localhost:7071/api/GetFromCosmos')
.then(res => res.json())
.then(data => {
    for(let row of data) {
        myDate = new Date(Number(row.timestamp) * 1000).toLocaleString();
        tabledata.innerHTML += `</td><td>${row.deviceId}</td><td>${myDate}</td><td>${row.temperature}</td><td>${row.humidity}</td><td>${row.name}</td><td>${row.school}</td>`
    }
})


