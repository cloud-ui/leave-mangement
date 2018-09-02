<template>
<div>
    <div class="add-tip">
        部门经理若无，则选择为总经理
    </div>
    <div style="padding-top:20px">
        <el-form :inline="true"  :model="formData" ref="ruleForm2" :rules="rule" class="demo-form-inline">
        <el-form-item label="部门名称" prop="name">
            <el-input  type="text" v-model="formData.name"  auto-complete="off"></el-input>
        </el-form-item>
        <el-form-item label="部门人数" prop="workerCount">
            <el-input :readonly="readonly" type="text" v-model="formData.workerCount" auto-complete="off"></el-input>
        </el-form-item>
        <el-form-item label="部门经理" prop="mangerId">
            <el-select style="width:90%" v-model="formData.mangerId" filterable placeholder="请选择">
                <el-option  v-for="item in workerList" :key="item.id" :label="item.name" :value="item.id">
                    <span>{{item.name}}</span> 
                    <span style="color: #c0c4cc;font-size: 12px;">({{item.position}})</span>
                </el-option>
            </el-select>
        </el-form-item>
    </el-form>
    <div style="display:flex;justify-content: flex-end;">
            <el-button :loading="loading" type="primary" @click="submitForm('ruleForm2')">提交</el-button>
            <el-button @click="resetForm('ruleForm2')">重置</el-button>
        </div>
    </div>    
</div>
    
</template>
<script>
import {FileApi} from '../api.js'
import '../dangan.scss'
export default {
    props:['formInfo'],
    data(){
        return{
            loading:false,
            readonly:this.formInfo.type==='edit'?true:false,
            formData:this.formInfo.formData,
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
                     if(this.formInfo.type === 'add'){
                        FileApi.addSingleDep(this.formData).then(res=>{
                        const type1 =res.data.isSuccess?'success':'error'
                        this.$message({
                            type: type1,
                            message: res.data.message
                        });
                     })
                     }else if(this.formInfo.type === 'edit'){
                         FileApi.editDeparment(this.formData).then(res=>{
                            const type1 =res.data.isSuccess?'success':'error'
                            this.$message({
                                type: type1,
                                message: res.data.message
                            });
                         })
                     }
                     this.closeForm()
                 }
             })
        },
        //关闭弹框
        closeForm(){
            this.$emit('close',false)
        },
        //清空表单
        resetForm(form){
            this.loading = false
            this.$refs[form].resetFields()
        }
    }
}
</script>

