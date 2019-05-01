<template>
 <div class="x-bar">
    <div :id="id" class="high" :option="option1"></div>
  </div>
</template>
<script>
  // 导入chart组件
// import XChart from '@/packages/components/charts'
import HighCharts from 'highcharts'
import {ShouyeApi} from './api.js'
  export default {
    data() {
      return {
        id:'high',
        option1:{
          chart: {
            type: 'column'
          },
          title: {
            text: "今日出勤情况"
          },
          subtitle: {
            text: null
          },
          xAxis: {
            categories:[],
            crosshair: true
          },
          yAxis: {
            min: 0,
            title: {
              text: '人数 (个)'
            },
            allowDecimals:false
          },
          tooltip: {
            // head + 每个 point + footer 拼接成完整的 table
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
            '<td style="padding:0"><b>{point.y:.1f} 人</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
          },
          plotOptions: {
            column: {
              borderWidth: 0
            }
          },
          series: []
        },
      }
    },
mounted(){
    this.LoadAttendanceData()
},
methods:{
     LoadAttendanceData(){
                ShouyeApi.getAttendanceData().then(res=>{
                    res.data.xData.map(val=>{
                        this.option1.xAxis.categories.push(val)
                    })
                    this.option1.series.push({...res.data.totalData})
                    this.option1.series.push({...res.data.clockData})
                    HighCharts.chart(this.id,this.option1)
                })
            }
},
    components: {
    }
  }
</script>
