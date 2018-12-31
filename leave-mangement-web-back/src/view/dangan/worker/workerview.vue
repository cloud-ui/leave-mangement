<template>
    <div class="worker-message-box">
        <div>
            <el-col :span="8" class="worker-message-box-part">
                <p>姓名：</p><span>{{data.name}}</span>
                <span class="auth" style="color:#5fb878;" v-if="data.isAuth === true"><i class="el-icon-success"></i>已认证</span>
                <span class="auth" v-else style="color:red;"><i class="el-icon-warning"></i>未认证</span>
            </el-col>
            <el-col :span="8" class="worker-message-box-part">
                <p>员工账号：</p><span>{{data.account}}</span>
            </el-col>
            <el-col :span="8" class="worker-message-box-part">
                <p>性别：</p><span>{{data.sex}}</span>
            </el-col>
        </div>
        <div>
            <el-col :span="8" class="worker-message-box-part">
                <p>职位：</p><span>{{data.position}}</span>
            </el-col>
            <el-col :span="8" class="worker-message-box-part">
                <p>所在部门：</p><span>{{data.deparment}}</span>
            </el-col>
            <el-col :span="8" class="worker-message-box-part">
                <p>工作状态：</p><span>{{data.state}}</span>
            </el-col>
        </div>
        <div>
            <el-col :span="8" class="worker-message-box-part">
                <p>年龄：</p><span>{{data.age}}</span>
            </el-col>
            <el-col :span="8" class="worker-message-box-part">
                <p>出生日期：</p><span>{{data.birth | formatDate}}</span>
            </el-col>
            <el-col :span="8" class="worker-message-box-part">
                <p>入职时间：</p><span>{{data.entryTime | formatDate}}</span>
            </el-col>
        </div>
        <div>
            <el-col :span="16" class="worker-message-box-part">
                <p>证件号码：</p><span>{{data.paperNumber}}</span>
                <span class="paper-type">{{data.paperType}}</span>
            </el-col>
            <el-col :span="8" class="worker-message-box-part">
                <p>联系方式：</p><span>{{data.phoneNumber}}</span>
            </el-col>
            <el-col :span="24" class="worker-message-box-part">
                <p>居住地址：</p><span>{{data.address}}</span>
            </el-col>
        </div>
    </div>
</template>
<script>
import '../dangan.scss'
import '../../index.scss'
import {FileApi} from '../api.js'
export default {
    props:['id'],
    data(){
        return{
            data:{}
        }
    },
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
    methods:{
        loadData(id){
            FileApi.getWorkerMessage(id).then(res=>{
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

