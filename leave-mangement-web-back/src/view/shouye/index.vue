<template>
<div>
    <div class="kq-home-body">
        <div class="kq-home-body-left">
            <comp-card borderColor="#fff" style="width:100%;height:200px;">
                <div slot="header">
                    <i style="padding-right:5px" class="iconfont icon-ziliao"></i><span>公司资料</span>
                </div>
                <div>
                    <ul>
                        <li><p class="company-message">公司名称：<span>{{company.name}}</span></p></li>
                        <li><p class="company-message">联系电话：<span>{{company.cellphoneNumber}}</span></p></li>
                        <li><p class="company-message">公司地址：<span>{{company.address}}</span></p></li>
                    </ul>
                </div>
            </comp-card>
            <comp-card borderColor="#fff" style="width:100%;height:300px;margin-top: 20px;">
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
        <div class="kq-home-body-right">
            <comp-count :workerCount="company.wokerCount" :deparmentCount="company.deparmentCount"></comp-count>
            <comp-card borderColor="#00a65a" style="height:110px;width:100%;">
                <div slot="header">
                    <i style="padding-right:5px" class="iconfont icon-steps"></i><span>申请流程</span>
                </div>
                <el-steps>
                    <el-step title="提交申请"></el-step>
                    <el-step title="审核"></el-step>
                    <el-step title="查看结果"></el-step>
                </el-steps>
            </comp-card>
            <comp-card borderColor="#409eff" style="width:100%;height:450px;margin-top: 20px;">
                <div slot="header">
                    <i style="padding-right:5px" class="iconfont icon-icon1"></i><span>出勤情况</span>
                </div>
                <div>
                    <comp-line-chart></comp-line-chart>
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
import CompLineChart from './lineCharts'
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
            xData:[],
            data:[],
            noticeData:[],
            noticeItem:{},
            dialogVisible:false,
            company:{},
        }
    },
    mounted(){
        this.loadData()
    },
    methods:{
       loadData(){
           this.loadNoticeData()
           this.loadCompany()
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
       loadCompany(){
           ShouyeApi.getCompany().then(res=>{
               this.company={...res.data}
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