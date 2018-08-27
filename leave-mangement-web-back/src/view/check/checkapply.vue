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
                    <p>请假类型：</p><span>{{data.typeName}}</span>
                </el-col>
            </div>
            <div>
                <el-col :span="7" class="approval-message-body-part">
                    <p>申请时间：</p><span>{{data.createTime}}</span>
                </el-col>
            </div>
            <div>
                <el-col :span="24" class="approval-message-body-part">
                    <p>理由：</p><pre v-html="data.content" style="margin:0px"></pre>
                </el-col>
            </div>
        </div>
        </div>
        <div class="check-btn" style="padding-top:10px">
                <el-button @click="pushCheck()" type="primary">递交上级</el-button>
                <el-button @click="checkApplication(true)" type="primary">同意</el-button>
                <el-button @click="checkApplication(false)" type="danger">拒绝</el-button>
        </div>
    </div>
</template>
<script>
import './check.scss'
import {CheckApi} from './api.js'
export default {
    props:['data'],
    data(){
        return{
            remark:'',
        }
    },
    methods:{
        
        checkApplication(val){
            const params={
                remark:this.remark,
                applicationId:this.data.id,
                type:this.data.type,
                IsAgree:val
            }
            CheckApi.checkApplication(params).then(res=>{
                const type1 = res.data.isSuccess?'success':'error'
                this.$message({
                    type:type1,
                    message:res.data.message
                })
                this.close()
            })
        },
        pushCheck(){
            const params={
                remark:this.remark,
                applicationId:this.appId,
            }
            CheckApi.pushCheck(params).then(res=>{
                const type1 = res.data.isSuccess?'success':'error'
                this.$message({
                    type:type1,
                    message:res.data.message
                })
                this.close()
            })
        },
        close(){
            this.$emit('close',false)
        }
    },
}
</script>

