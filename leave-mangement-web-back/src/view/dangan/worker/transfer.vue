<template>
    <div>
        <div><span class="worker-name">{{data.name}}</span>将被调整为：</div>
        <div class="teansfer-body">
            <el-form :model="data" :inline="true" class="demo-form-inline" label-width="90px">
                <el-form-item label="部门：">
                    <comp-dep-select v-model="data.deparmentId" @change="changeDep"></comp-dep-select>
                </el-form-item>
                <el-form-item label="职位：">
                    <comp-option-select v-model="data.positionId" @change="changeOption"></comp-option-select>
                </el-form-item>
                <el-form-item label="职位状态：">
                    <comp-state-select v-model="data.stateId" @change="changeState"></comp-state-select>
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
                this.data = {
                    deparmentId:res.data.deparmentId,
                    stateId:res.data.stateId,
                    positionId:res.data.positionId}
            })
        },
        submitForm(){
            this.loading = true
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
