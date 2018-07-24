<template>
<div>
    <comp-count></comp-count>
    <div class="kq-home-body">
        <div class="kq-home-body-left">
            <comp-card borderColor="#409eff" style="width:100%;">
                <div slot="header">
                    <i style="padding-right:5px" class="iconfont icon-icon1"></i><span>出勤情况</span>
                </div>
                <div>
                    <comp-line-chart :xData="xData" :data="data"></comp-line-chart>
                </div>            
            </comp-card>
        </div>
        <div class="kq-home-body-right">
            <comp-card borderColor="#00a65a" style="width:100%;height:300px;">
                <div slot="header">
                    <i style="padding-right:5px" class="iconfont icon-gonggao"></i><span>公司公告</span>
                </div>
                <div>
                    555
                </div>            
            </comp-card>
        </div>
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
            data:[{
                name:'实际人数',
                data:[10,20,30,40,50,60,70]
            },{
                name:'出勤人数',
                data:[5,10,15,20,25,30,35]
            }]
        }
    },
    mounted(){
    },
    methods:{
       
    }
}
</script>