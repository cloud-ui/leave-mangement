<template>
<div>
    <div class="add-tip">
        部门经理若无，则选择为总经理
    </div>
    <div style="padding-top:20px">
        <el-form style="width:60%;margin:auto;" :model="formData" ref="ruleForm2" :rules="rule" label-width="80px" class="demo-ruleForm">
        <el-form-item label="部门名称" prop="name">
            <el-input  type="text" v-model="formData.name"  auto-complete="off"></el-input>
        </el-form-item>
        <el-form-item label="部门经理" prop="mangerId">
            <el-select style="width:100%" v-model="formData.mangerId" filterable placeholder="请选择">
                <el-option  v-for="item in workerList" :key="item.id" :label="item.name" :value="item.id"> </el-option>
            </el-select>
        </el-form-item>
        <el-form-item label="部门人数" prop="workerCount">
            <el-input  type="text" v-model="formData.workerCount" auto-complete="off"></el-input>
        </el-form-item>
        <el-form-item>
            <el-button :loading="loading" type="primary" @click="submitForm('ruleForm2')">提交</el-button>
            <el-button @click="resetForm('ruleForm2')">重置</el-button>
        </el-form-item>
    </el-form>
    </div>    
</div>
    
</template>
<script>
import {FileApi} from '../api.js'
import '../dangan.scss'
export default {
    data(){
        return{
            loading:false,
            formData:{
                name:'',
                mangerId:0,
                workerCount:0,
            },
            workerList:{},
            rule:{
                name:[{required: true, message: '请输入名称', trigger: 'blur'}],
                mangerId:[{required: true, message: '请选择部门经理', trigger: 'blur'}]
            }
        }
    },
    mounted(){
        this.loadWorkerList();
    },
    methods:{
        loadWorkerList(){
            FileApi.getWorkerList().then(res=>{
                this.workerList={
                    ...res.data
                }
            })
        },
        submitForm(form){
             this.$refs[form].validate((valid) => {
                 if(valid){
                     this.loading = true;
                     FileApi.addSingleDep(this.formData).then(res=>{
                         console.log(res.data)
                     })
                 }
             })
        },
        resetForm(form){

        }
    }
}
</script>

