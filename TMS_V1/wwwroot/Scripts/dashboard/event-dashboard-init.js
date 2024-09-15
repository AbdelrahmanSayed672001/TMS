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


	// Events Metrics
	var options = {
		chart: {
			height: 350,
			type: 'line',
			fontFamily: 'IBM Plex Sans, sans-serif',
			foreColor: '#6780B1',

		},
		plotOptions: {
			bar: {
				horizontal: false,
				columnWidth: '50%',
				endingShape: 'rounded'
			},
		},
		dataLabels: {
			enabled: true
		},
		stroke: {
			show: true,
			width: 1,
			colors: ['transparent']
		},
		series: [{
			name: 'Tickets Sold',
			data: [44, 55, 57, 56, 61, 58, 63, 60, 66]
		}, {
			name: 'Tickets Available',
			data: [76, 85, 10, 98, 87, 10, 91, 14, 94]
		}, {
			name: 'Net Revenue',
			data: [35, 41, 36, 26, 45, 48, 52, 53, 41]
		}],
		colors: ['#5D78FF', '#C9D5FA', '#63CF72'],
		markers: {
			size: 5,
			strokeColor: "#fff",
			strokeWidth: 3,
			strokeOpacity: 1,
			fillOpacity: 1,
			hover: {
				size: 6
			}
		},
		xaxis: {
			categories: ['Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct'],
		},

		yaxis: {
			title: {
				text: 'K'
			}
		},
		fill: {
			opacity: 1

		},
		tooltip: {
			y: {
				formatter: function (val) {
					return "" + val + "K"
				}
			}
		}
	}

	var chart = new ApexCharts(
		document.querySelector("#audienceOverviewBar"),
		options
	);

	chart.render();


	// Events Interest
	var piedata = [{
			label: "Male",
			fontFamily: 'IBM Plex Sans, sans-serif',
			foreColor: '#6780B1',
			data: [
				[1, 95]
			],
			color: '#5D78FF'
		},
		{
			label: "Female",
			data: [
				[1, 75]
			],
			color: '#C9D5FA'
		},
		{
			label: "VIP",
			data: [
				[1, 60]
			],
			color: '#63CF72'
		},
		{
			label: "Others",
			data: [
				[1, 50]
			],
			color: '#EE8CE5'
		}
	];

	$.plot('#sessionsDeviceDonut', piedata, {
		series: {
			pie: {
				show: true,
				radius: 1,
				label: {
					show: true,
					radius: 2 / 3,
					formatter: labelFormatter,
					threshold: 0.1
				}
			}
		},
		grid: {
			hoverable: true,
			clickable: true
		}
	});


	function labelFormatter(label, series) {
		return "<div style='font-size:7pt; text-align:center; padding:2px; color:white;'>" + label + "<br/>" + Math.round(series.percent) + "%</div>";
	}


});