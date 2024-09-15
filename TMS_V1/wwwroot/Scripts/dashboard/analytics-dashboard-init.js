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


	// Audience Overview	
	var options = {
		chart: {
			height: 350,
			type: 'bar',
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
			enabled: false
		},
		stroke: {
			show: true,
			width: 2,
			colors: ['transparent']
		},
		series: [{
			name: 'New Visitors',
			data: [44, 55, 57, 56, 61, 58, 63, 60, 66]
		}, {
			name: 'Unique Visitors',
			data: [76, 85, 101, 98, 87, 105, 91, 114, 94]
		}, {
			name: 'Returning Visitors',
			data: [35, 41, 36, 26, 45, 48, 52, 53, 41]
		}],
		colors: ['#5D78FF', '#C9D5FA', '#63CF72'],
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


	// Sessions By Channel
	var piedata = [{
			label: "New User",
			fontFamily: 'IBM Plex Sans, sans-serif',
			foreColor: '#6780B1',
			data: [
				[1, 50]
			],
			color: '#5D78FF'
		},
		{
			label: "Page Views",
			data: [
				[1, 40]
			],
			color: '#C9D5FA'
		},
		{
			label: "Page Session",
			data: [
				[1, 90]
			],
			color: '#63CF72'
		},
		{
			label: "Bounce Rate",
			data: [
				[1, 70]
			],
			color: '#EE8CE5'
		}
	];

	$.plot('#sessionsDeviceDonut', piedata, {
		series: {
			pie: {
				show: true,
				radius: 1,
				innerRadius: 0.5,
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


	/* Acquisition Area Chart */
	var options = {
		chart: {
			type: "area",
			height: 200,
			fontFamily: 'IBM Plex Sans, sans-serif',
			foreColor: '#6780B1',
			toolbar: {
				show: false
			},

		},
		colors: ['rgba(40, 167, 69, .10)', 'rgba(5, 88, 183, 0.10)'],
		stroke: {
			curve: "smooth",
			width: 3
		},
		dataLabels: {
			enabled: false
		},
		series: [{
			name: 'New Users',
			data: generateDayWiseTimeSeries(0, 18)
		}, {
			name: 'Returning Users',
			data: generateDayWiseTimeSeries(1, 18)
		}],
		markers: {
			size: 0,
			strokeColor: "#fff",
			strokeWidth: 3,
			strokeOpacity: 1,
			fillOpacity: 1,
			hover: {
				size: 6
			}
		},
		xaxis: {
			type: "datetime",
			axisBorder: {
				show: false
			},
			axisTicks: {
				show: false
			},
			tooltip: {
				enabled: false
			}
		},
		yaxis: {
			labels: {
				offsetX: 0,
				offsetY: 0
			},
			tooltip: {
				enabled: false
			}
		},
		grid: {
			padding: {
				left: 0,
				right: 0
			}
		},
		legend: {
			show: false,
		},
		fill: {
			type: "solid",
			fillOpacity: 1
		}
	};

	var chart = new ApexCharts(document.querySelector("#acquisitionAreaChart"), options);

	chart.render();

	function generateDayWiseTimeSeries(s, count) {
		var values = [
			[
				4, 3, 10, 9, 12, 20, 25, 9, 12, 7, 19, 5, 13, 9, 17, 2, 7, 5
			],
			[
				2, 3, 8, 7, 22, 16, 23, 7, 11, 5, 12, 5, 10, 4, 15, 2, 6, 2
			]
		];
		var i = 0;
		var series = [];
		var x = new Date("11 May 2019").getTime();
		while (i < count) {
			series.push([x, values[s][i]]);
			x += 86400000;
			i++;
		}
		return series;
	}


	/* Behavior Area Chart */
	function generateDayWiseTimeSeries(s, count) {
		var values = [
			[180, 140, 120, 135, 155, 170, 180, 150, 140, 150, 130, 130, 180, 140, 120, 135, 155, 170, 180, 150, 140, 150, 130, 130],
			[150, 110, 90, 115, 125, 160, 160, 140, 100, 110, 120, 120, 150, 110, 90, 115, 125, 160, 160, 140, 100, 110, 120, 120]
		];
		var i = 0;
		var series = [];
		var x = new Date("11 May 2019").getTime();
		while (i < count) {
			series.push([x, values[s][i]]);
			x += 86400000;
			i++;
		}
		return series;
	}


	var options = {
		chart: {
			type: "area",
			height: 200,
			fontFamily: 'IBM Plex Sans, sans-serif',
			foreColor: '#6780B1',
			toolbar: {
				show: false
			},

		},
		colors: ['rgba(220, 53, 69, .10)', 'rgba(234, 147, 12, 0.1)'],
		stroke: {
			curve: "smooth",
			width: 3
		},
		dataLabels: {
			enabled: false
		},
		series: [{
			name: 'Bounce Rate',
			data: generateDayWiseTimeSeries(0, 18)
		}, {
			name: 'Avg. Session',
			data: generateDayWiseTimeSeries(1, 18)
		}],
		markers: {
			size: 0,
			strokeColor: "#fff",
			strokeWidth: 3,
			strokeOpacity: 1,
			fillOpacity: 1,
			hover: {
				size: 6
			}
		},
		xaxis: {
			type: "datetime",
			axisBorder: {
				show: false
			},
			axisTicks: {
				show: false
			},
			tooltip: {
				enabled: false
			}
		},
		yaxis: {
			labels: {
				offsetX: 0,
				offsetY: 0
			},
			tooltip: {
				enabled: false
			}
		},
		grid: {
			padding: {
				left: 0,
				right: 0
			}
		},

		legend: {
			show: false,
		},
		fill: {
			type: "solid",
			fillOpacity: 1
		}
	};

	var chart = new ApexCharts(document.querySelector("#behaviorAreaChart"), options);

	chart.render();


	/* Revenue Area Chart */
	var options = {
		chart: {
			type: "area",
			height: 200,
			fontFamily: 'IBM Plex Sans, sans-serif',
			foreColor: '#6780B1',
			toolbar: {
				show: false
			},

		},
		colors: ['rgba(40, 167, 69, .10)', 'rgba(234, 147, 12, 0.1)'],
		stroke: {
			curve: "smooth",
			width: 3
		},
		dataLabels: {
			enabled: false
		},
		series: [{
			name: 'Transiaction',
			data: generateDayWiseTimeSeries(0, 18)
		}, {
			name: 'Revenue',
			data: generateDayWiseTimeSeries(1, 18)
		}],
		markers: {
			size: 0,
			strokeColor: "#fff",
			strokeWidth: 3,
			strokeOpacity: 1,
			fillOpacity: 1,
			hover: {
				size: 6
			}
		},
		xaxis: {
			type: "datetime",
			axisBorder: {
				show: false
			},
			axisTicks: {
				show: false
			},
			tooltip: {
				enabled: false
			}
		},
		yaxis: {
			labels: {
				offsetX: 0,
				offsetY: 0
			},
			tooltip: {
				enabled: false
			}
		},
		grid: {
			padding: {
				left: 0,
				right: 0
			}
		},

		legend: {
			show: false,
		},
		fill: {
			type: "solid",
			fillOpacity: 1
		}
	};


	var chart = new ApexCharts(document.querySelector("#revenueAreaChart"), options);

	chart.render();


	// Region GeoCharts
	google.charts.load('current', {
		'packages': ['geochart'],
		// Note: you will need to get a mapsApiKey for your project.
		// See: https://developers.google.com/chart/interactive/docs/basic_load_libs#load-settings
		'mapsApiKey': 'AIzaSyD-9tSrke72PouQMnMX-a7eZSW0jkFMBWY'
	});
	google.charts.setOnLoadCallback(drawRegionsMap);

	function drawRegionsMap() {
		var data = google.visualization.arrayToDataTable([
			['Country', 'Popularity'],
			['Germany', 200],
			['United States', 300],
			['Brazil', 400],
			['Canada', 500],
			['France', 600],
			['RU', 700]
		]);

		var options = {
			colors: ['#5D78FF', '#C9D5FA'],
		};

		var chart = new google.visualization.GeoChart(document.getElementById('regionGeoCharts'));

		chart.draw(data, options);
	}


	// Realtime Traffics
	window.Apex = {
		colors: ['#5D78FF'],
		xaxis: {
			axisTicks: {
				color: '#E4EAFD'
			},
			axisBorder: {
				color: "#E4EAFD"
			}
		},

		yaxis: {
			decimalsInFloat: 2,
			opposite: true,
			labels: {
				offsetX: -10
			}
		}
	};


	var trigoStrength = 1
	var iteration = 10

	function getRandom() {
		var i = iteration;
		return (Math.sin(i / trigoStrength) * (i / trigoStrength) + i / trigoStrength + 1) * (trigoStrength * 2)
	}

	function getRangeRandom(yrange) {
		return Math.floor(Math.random() * (yrange.max - yrange.min + 1)) + yrange.min
	}

	function generateMinuteWiseTimeSeries(baseval, count, yrange) {
		var i = 0;
		var series = [];
		while (i < count) {
			var x = baseval;
			var y = ((Math.sin(i / trigoStrength) * (i / trigoStrength) + i / trigoStrength + 1) * (trigoStrength * 2))

			series.push([x, y]);
			baseval += 100000;
			i++;
		}
		return series;
	}


	function getNewData(baseval, yrange) {
		var newTime = baseval + 100000;
		return {
			x: newTime,
			y: Math.floor(Math.random() * (yrange.max - yrange.min + 1)) + yrange.min
		}
	}

	var optionsColumn = {
		chart: {
			height: 350,
			type: 'bar',
			fontFamily: 'IBM Plex Sans, sans-serif',
			foreColor: '#6780B1',
			animations: {
				enabled: true,
				easing: 'linear',
				dynamicAnimation: {
					speed: 1000
				}
			},
			// dropShadow: {
			//   enabled: true,
			//   left: -14,
			//   top: -10,
			//   opacity: 0.05
			// },
			events: {
				animationEnd: function (chartCtx) {
					const newData = chartCtx.w.config.series[0].data.slice()
					newData.shift()
					window.setTimeout(function () {
						chartCtx.updateOptions({
							series: [{
								data: newData
							}],
							xaxis: {
								min: chartCtx.minX,
								max: chartCtx.maxX
							},
							subtitle: {
								text: parseInt(getRangeRandom({
									min: 1,
									max: 20
								})).toString() + '%',
							}
						}, false, false)
					}, 300)
				}
			},

			toolbar: {
				show: false
			},
			zoom: {
				enabled: false
			}
		},
		colors: ['#5D78FF'],
		dataLabels: {
			enabled: false
		},
		stroke: {
			width: 0,
		},
		series: [{
			name: 'Load Average',
			data: generateMinuteWiseTimeSeries(new Date("12/12/2016 00:20:00").getTime(), 12, {
				min: 10,
				max: 110
			})
		}],


		xaxis: {
			type: 'datetime',
			range: 5000000
		},
		legend: {
			show: true
		},
	}


	var chartColumn = new ApexCharts(
		document.querySelector("#realtimeTrafficsChart"),
		optionsColumn
	);
	chartColumn.render()


	window.setInterval(function () {


		iteration++;

		chartColumn.updateSeries([{
			data: [...chartColumn.w.config.series[0].data, [
				chartColumn.w.globals.maxX + 50000,
				getRandom()
			]]
		}])


	}, 3000)


	/* Visitors By Devices */
	var options = {
		chart: {
			height: 225,
			type: 'bar',
			fontFamily: 'IBM Plex Sans, sans-serif',
			foreColor: '#6780B1',
			toolbar: {
				show: false
			},
		},

		plotOptions: {
			bar: {
				horizontal: false,
				columnWidth: '25%',
				endingShape: 'rounded'
			},
		},
		dataLabels: {
			enabled: false
		},
		stroke: {
			show: true,
			width: 1,
			colors: ['transparent']
		},
		series: [{
			name: 'Desktop',
			data: [144, 155, 157, 156, 161, 170]
		}, {
			name: 'Mobile',
			data: [176, 185, 101, 198, 187, 190]
		}, {
			name: 'Others',
			data: [135, 141, 136, 126, 145, 150]
		}],
		colors: ['#5D78FF', '#C9D5FA', '#63CF72'],
		xaxis: {
			categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
		},

		fill: {
			opacity: 1

		},
		legend: {
			show: false,
			position: 'top',
			offsetY: 0,
		},

	}

	var chart = new ApexCharts(
		document.querySelector("#visitorsByDevices"),
		options
	);

	chart.render();


});