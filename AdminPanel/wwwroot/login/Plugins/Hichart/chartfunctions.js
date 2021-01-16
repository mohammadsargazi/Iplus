function columnChart(containerId, dataMedian, dataPercentile, dataMinimum, title) {
    debugger;
    Highcharts.chart(containerId, {
        chart: {
            type: 'column'
        },
        tooltip: {
            pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.y}</b> ({point.percentage:.0f}%)<br/>',
            shared: true
        },
        title: {
            text: title
        },
        tooltip: {

            shared: true
        },
        plotOptions: {
            column: {
                stacking: 'count'
            }
        },
        colors: ['#a2d9da', '#48c8dd', '#0081a7'],
        series: [{
            name: 'Median',
            data: dataMedian
        }, {
            name: '95th Percentile',
            data: dataPercentile
        }, {
            name: 'Minimum',
            data: dataMinimum
        }]
    });
    $("#" + containerId).css("overflow", "visible");
}

function lineChart(containerId, dataMedian, dataPercentile, dataMinimum, title) {
    debugger;
    Highcharts.chart(containerId, {
        chart: {
            type: 'line'
        },
        tooltip: {
            pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.y}</b> ({point.percentage:.0f}%)<br/>',
            shared: true
        },
        title: {
            text: title
        },
        tooltip: {

            shared: true
        },
        plotOptions: {
            column: {
                stacking: 'count'
            }
        },
        colors: ['#a2d9da', '#48c8dd', '#0081a7'],
        series: [{
            name: 'Median',
            data: dataMedian
        }, {
            name: '95th Percentile',
            data: dataPercentile
        }, {
            name: 'Minimum',
            data: dataMinimum
        }]
    });
    $("#" + containerId).css("overflow", "visible");
}


function OneColumnChart(containerId, data, title) {
    debugger;
    var series = new Array();
    for (var i = 0; i < data.split(';').length; i++) {

        var newArray = new Array(data.split(';')[i].split(',')[0].toString(), parseFloat(data.split(';')[i].split(',')[1]));
        series.push(newArray);
    }

    Highcharts.chart(containerId, {
        chart: {
            type: 'column'
        },
        tooltip: {
            pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.y}</b> ({point.percentage:.0f}%)<br/>',
            shared: true
        },
        title: {
            text: title
        },
        tooltip: {
            valueDecimals: 2,
        },
        accessibility: {
            announceNewData: {
                enabled: true
            }
        },

        xAxis: {
            categories: "category"
        },
        dataLabels: {
            enabled: true,
            formatter: function () {
                return Highcharts.numberFormat(this.y, 2);
            }
        },
        yAxis: {
            labels: {
                format: '{value:.2f}'
            }
        },
        legend: {
            enabled: false
        },
        plotOptions: {
            column: {
                stacking: 'count'
            }
        },
        series: [{
            name: series.name,
            data: series,
            tooltip: {
                formatter: function () {
                    return this.series.name + ': <b>' + Highcharts.numberFormat(this.y, 2) + '%</b>';
                }
            }
        }]
    });
    $("#" + containerId).css("overflow", "visible");
}

