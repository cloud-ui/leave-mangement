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
                <el-col :span="17" class="approval-message-body-part">
                    <p>起止时间：</p><span>{{data.startTime|formatDate}} - {{data.endTime|formatDate}}</span>
                </el-col>
            </div>
            <div>
                <el-col :span="24" class="approval-message-body-part">
                    <p>请假理由：</p><span>{{data.content}}</span>
                </el-col>
            </div>
        </div>
        </div>
        <div class="check">
            <el-form label-width="80px">
                <el-form-item label="备注：">
                    <el-input type="textarea" v-model="remark" :rows="2"></el-input>
                </el-form-item>
                <el-form-item class="check-btn">
                    <el-button @click="pushCheck()" type="primary">递交上级</el-button>
                    <el-button @click="checkApplication(true)" type="primary">同意</el-button>
                    <el-button @click="checkApplication(false)" type="danger">拒绝</el-button>
                </el-form-item>
            </el-form>
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
    mounted() {
        console.log("333")
        console.log(this.data)
    },
    methods:{
        loadData(){
            CheckApi.getApplication(this.appId).then(res=>{
                this.data = {...res.data}
            })
        },
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

