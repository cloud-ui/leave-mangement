<template>
    <div id="chart" style="width:100%;height:400px;">
    </div>
</template>
<script>
import {ShouyeApi} from './api.js'
    export default {
        mounted() {
            this.LoadAttendanceData()
            this.drawLine()
        },
        data(){
            return{
                seriesData:[],
                legendData:[],
                data:[],
                xData:[]
            }
        },
        methods: {
            drawLine() {
                console.log(this.seriesData)
                console.log(this.legendData)
                console.log("***22")
                console.log(this.xData)
                // 基于准备好的dom，初始化echarts实例
                let myChart = this.$echarts.init(document.getElementById('chart'))
                // 绘制图表
                myChart.setOption({
                    // title: {
                    //     text: '折线图堆叠'
                    // },
                    tooltip: {
                        trigger: 'axis'
                    },
                    //线条注释
                    legend: {
                        data: this.legendData
                    },
                    grid: {
                        left: '1%',
                        right: '2%',
                        bottom: '3%',
                        containLabel: true
                    },
                    //保存为图片
                    // toolbox: {
                    //     feature: {
                    //         saveAsImage: {}
                    //     }
                    // },
                    xAxis: {
                        type: 'category',
                        boundaryGap: false,
                        data: this.xData
                    },
                    yAxis: {
                        type: 'value'
                    },
                    series: this.seriesData
                });
            },
            LoadAttendanceData(){
                ShouyeApi.getAttendanceData().then(res=>{
                    debugger
                    res.data.xData.map(val=>{
                        this.xData.push(val)
                    })
                    this.data.push(res.data.totalData)
                    this.data.push(res.data.clockData)
                    this.setSeriesData()
                })
            },
            setSeriesData(){
                this.data.map(value=>{
                    this.seriesData.push({
                            name: value.name,
                            type: 'line',
                            data: value.data
                    })
                    this.legendData.push(value.name)
                    console.log("9999")
                    console.log(this.legendData)
                })
            }
        }
    }
</script>

