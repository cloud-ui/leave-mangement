<template>
<div>
    <div class="approval-message">
        <p class="approval-message-title">申请详细信息</p>
        <div class="approval-message-body">
            <div>
                <el-col :span="7" class="approval-message-body-part">
                <p>姓名：</p><span>{{data.workerName}}</span>
                </el-col>
                <el-col :span="7" class="approval-message-body-part">
                    <p>部门：</p><span>{{data.deparment}}</span>
                </el-col>
                <el-col :span="10" class="approval-message-body-part">
                    <p>请假类型：</p><span>{{data.type}}</span>
                </el-col>
            </div>
            <div>
                <el-col :span="7" class="approval-message-body-part">
                <p>请假状态：</p>
                <span v-if="data.state===1" class="approval-state unlook">{{data.stateName}}</span>
                <span v-if="data.state===2" class="approval-state agree">{{data.stateName}}</span>
                <span v-if="data.state===3" class="approval-state refuse">{{data.stateName}}</span>
                </el-col>
                <el-col :span="7" class="approval-message-body-part">
                    <p>申请时间：</p><span>{{data.createTime}}</span>
                </el-col>
                <el-col :span="10" class="approval-message-body-part">
                    <p>起止时间：</p><span>{{data.startTime|formatDate}} - {{data.endTime|formatDate}}</span>
                </el-col>
            </div>
            <div>
                <el-col :span="24" class="approval-message-body-part">
                    <p>请假理由：</p><span>{{data.account}}</span>
                </el-col>
            </div>
        </div>
    </div>
    <div style="margin-top: 20px;" class="approval-message">
        <p class="approval-message-title">处理详细信息</p>
        <div class="approval-message-body">
            <div>
                <el-col :span="7" class="approval-message-body-part">
                <p>处理人：</p><span>{{data.handerName}}</span>
                </el-col>
                <el-col :span="7" class="approval-message-body-part">
                <p>处理时间：</p><span>{{data.handerTime}}</span>
                </el-col>
            </div>
            <div>
                <el-col :span="24" class="approval-message-body-part">
                    <p>处理备注：</p><span>{{data.remark}}</span>
                </el-col>
            </div>
        </div>
    </div>
</div>
</template>
<script>
import '../approval.scss'
import { ApprovalApi } from "../api.js";
export default {
    props:['id'],
    watch:{
        id:{
        handler(val, olaval) {
          this.loadData(val)
        },
        deep: true
        },
    },
    mounted(){
        this.loadData(this.id)
    },
    data(){
        return{
            data:{}
        }
    },
    methods:{
        loadData(id){
            ApprovalApi.getApplication(id).then(res=>{
                this.data = res.data
            })
        }
    },
    filters: {
            formatDate: function (value) {
                let date = new Date(value);
                let y = date.getFullYear();
                let MM = date.getMonth() + 1;
                MM = MM < 10 ? ('0' + MM) : MM;
                let d = date.getDate();
                d = d < 10 ? ('0' + d) : d;
                return y + '-' + MM + '-' + d;
                }
            }
}
</script>

