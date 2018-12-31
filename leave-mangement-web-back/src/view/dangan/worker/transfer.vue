<template>
    <div>
        <div><span class="worker-name">{{userName}}</span>将被调整为：</div>
        <div class="teansfer-body">
            <el-form :model="data" :inline="true" class="demo-form-inline" label-width="90px">
                <el-form-item label="部门：">
                    <comp-dep-select v-model="data.deparmentId" @change="changeDep"></comp-dep-select>
                </el-form-item>
                <el-form-item label="职位：">
                    <comp-option-select v-model="data.positionId" @change="changeOption"></comp-option-select>
                </el-form-item>
                <el-form-item label="职位状态：">
                    <comp-state-select :stateId="data.stateId" v-model="data.stateId" @change="changeState"></comp-state-select>
                </el-form-item>
                <el-form-item class="btn">
                    <el-button>取消</el-button>
                    <el-button :loading="loading" type="primary" @click="submitForm" >确定</el-button>
                </el-form-item>
            </el-form>
        </div>
    </div>
</template>
<script>
import {FileApi} from '../api.js'
import CompDepSelect from '@/packages/components/dep-select'
import CompStateSelect from '@/packages/components/state-select'
import CompOptionSelect from '@/packages/components/option-select'
export default {
    props:['id'],
    components:{
        CompStateSelect,
        CompDepSelect,
        CompOptionSelect
    },
    data(){
        return{
            data:{
                deparmentId:'',
                stateId:'',
                positionId:'',
                workerId:this.id
            },
            userName:'',
            loading:false
        }
    },
    watch:{
        id:{
        handler(val, olaval) {
            this.data.workerId = val
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
                this.userName = res.data.name
                this.data.deparmentId=res.data.deparmentId
                this.data.stateId=res.data.stateId
                this.data.positionId=res.data.positionId
                console.log(res.data)
            })
            console.log(this.userName)
        },
        submitForm(){
            console.log(this.data)
            this.loading = true
            FileApi.TransferOfPersonnel(this.data).then(res=>{
                this.loading = false
                const type1 = res.data.isSuccess?'success':'error'
                this.$message({ type: type1,message: res.data.message});
            })
        },
        changeDep(id){
            this.data.deparmentId = id
        },
        changeOption(id){
            this.data.positionId = id
        },
        changeState(id){
            this.data.stateId = id
        }
    },
}
</script>
<style lang="scss" scoped>
.worker-name{
    color:red;
    padding-right: 4px;
}
.teansfer-body{
    padding-top: 10px;
}
.btn{
    display: flex;
    justify-content: flex-end;
}
</style>
