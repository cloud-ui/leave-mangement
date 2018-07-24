<template>
<div>
    <comp-count></comp-count>
    <div>
        <comp-card style="width:50%;">
            <div slot="header">
                <p>出勤情况</p>
            </div>
            <div>
                <comp-line-chart :xData="xData"></comp-line-chart>
            </div>            
        </comp-card>
    </div>
    
</div>
</template>
<script>
import CompCount from './count'
import CompLineChart from '../../packages/components/lineChart'
import CompCard from '../../packages/components/card'
export default{
    components:{
        CompCount,
        CompLineChart,
        CompCard
    },
    data(){
        return{
            xData:['周一', '周二', '周三', '周四', '周五', '周六', '周7'],
        }
    },
    mounted(){
    },
    methods:{
       
    }
}
</script>