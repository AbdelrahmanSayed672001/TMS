$(function () {
	'use strict';
	// Toster Notification         
	$(document).ready(function () {
		setTimeout(function () {
			toastr.options = {
				positionClass: 'toast-top-right',
				closeButton: true,
				progressBar: true,
				showMethod: 'slideDown',
				timeOut: 5000
			};
			toastr.info('Multipurpose Admin Template', 'Hi, welcome to Metrical');

		}, 300);

	});


	// Social Activity Zoomable TimeSeries
	var ts2 = 1484418600000;
	var dates = [];
	var spikes = [5, -5, 3, -3, 8, -8]
	for (var i = 0; i < 120; i++) {
		ts2 = ts2 + 86400000;
		var innerArr = [ts2, dataSeries[1][i].value];
		dates.push(innerArr)
	}

	var options = {
		chart: {
			type: 'area',
			stacked: false,
			height: 340,
			zoom: {
				type: 'x',
				enabled: true
			},
			toolbar: {
				show: false,
				autoSelected: 'zoom'
			}
		},
		dataLabels: {
			enabled: false
		},

		
		series: [{
			name: 'Social Activity',
			data: dates
		}],
		markers: {
			size: 0,
		},
		stock:{
			width: 1,
		},
		fill: {
			type: 'gradient',
			gradient: {
				shadeIntensity: 1,
				inverseColors: false,
				opacityFrom: 0.5,
				opacityTo: 0,
				stops: [0, 90, 100]
			},
		},
		yaxis: {
			min: 20000000,
			max: 250000000,
			labels: {
				formatter: function (val) {
					return (val / 1000000).toFixed(0);
				},
			},
			title: {
				text: 'Price'
			},
		},
		xaxis: {
			type: 'datetime',
		},

		tooltip: {
			shared: false,
			y: {
				formatter: function (val) {
					return (val / 1000000).toFixed(0)
				}
			}
		}
	}

	var chart = new ApexCharts(
		document.querySelector("#zoomableTimeSeries"),
		options
	);

	chart.render();

});