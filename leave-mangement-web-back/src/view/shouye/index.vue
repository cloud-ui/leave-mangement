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
                    <i style="padding-right:5px" class="iconfont icon-gonggao"></i><span>公告栏</span>
                </div>
                <div>
                    <div class="notice-item" v-for="item in noticeData" :key="item.id" @click="handleLookNotice(item)">
                        <span :class="item.typeCode === 1 ? `blue-color`:`green-color`">[{{item.type}}]</span>
                        <p>{{item.title}}</p>
                    </div>
                </div>            
            </comp-card>
        </div>
    </div>
    <el-dialog width="408px" :title="noticeItem.title" :visible.sync="dialogVisible" :before-close="handleClose">
         <comp-view @closeForm='handleClose' @close='closeForm' :notice="noticeItem" ref="compView"></comp-view>
    </el-dialog>
</div>
</template>
<script>
import CompCount from './count'
import CompLineChart from '../../packages/components/lineChart'
import CompCard from '../../packages/components/card'
import CompView from '../../packages/components/noticeview'
import {ShouyeApi} from './api.js'
import './shouye.scss'
export default{
    components:{
        CompCount,
        CompLineChart,
        CompCard,
        CompView
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
            }],
            noticeData:[],
            noticeItem:{},
            dialogVisible:false
        }
    },
    mounted(){
        this.loadData()
    },
    methods:{
       loadData(){
           this.loadNoticeData()
       },
       loadNoticeData(){
           const params = {
               currentPage: 1,
                currentPageSize: 5,
                query: '',
           }
           ShouyeApi.noticeList(params).then(res=>{
               this.noticeData = res.data.data
           })
       },
       handleLookNotice(val){
           this.noticeItem = val
           this.dialogVisible = true
       },
       //关闭弹框
            handleClose() {
                this.dialogVisible = false
            },
            closeForm(val) {
                this.loadData()
                this.dialogVisible = val
            },
    }
}
</script>