<template>
    <el-form ref="addForm" :model="form" :rules="rule" label-width="15%">
        <el-form-item prop="name" label="名称:">
            <el-input v-model="form.name"></el-input>
        </el-form-item>
        <el-form-item prop="pay" label="薪资:">
            <el-input v-model="form.pay"></el-input>
        </el-form-item>
        <el-form-item style="display: flex;justify-content: flex-end;">
            <el-button @click="closeForm()" >取消</el-button>
            <el-button @click="submitData()" :loading="loading"  type="primary" >提交</el-button>
        </el-form-item>
    </el-form>
</template>
<script>
import {FileApi} from '../api.js'
export default {
    props:['formInfo'],
    data(){
        return{
            form:this.formInfo.data,
            loading:false,
            rule:{
                name:[{required: true,message: '请输入名称',trigger: 'blur'}],
                pay:[{required:true,message:'请输入薪资',trigger:'blur'}]
                }
        }
    },
    watch:{
        formInfo: {
            handler(val, olaval) {
            this.form = val.data;
            },
            deep: true
        }
    },
    methods:{
        //点击提交按钮
        submitData(){
            this.$refs['addForm'].validate(valid => {
                if(valid){
                    const params = {
                        ...this.form
                    };
                    this.loading = true
                    //判断是职位的操作
                    if(this.formInfo.state === 'position'){
                        switch(this.formInfo.type){
                            case 'edit':
                            FileApi.editPosition(this.form).then(res=>{
                                this.closeForm()
                                this.$message({
                                    type: 'success',
                                    message: res.data.message
                                });
                            })
                            break;
                            case 'add':
                            FileApi.addPosition(this.form).then(res=>{
                                this.closeForm()
                                this.$message({
                                    type: 'success',
                                    message: res.data.message
                                });
                            })
                            break;
                            default:
                        }
                    }else if(this.formInfo.state === 'state'){
                        switch(this.formInfo.type){
                            case 'edit':
                            FileApi.editState(this.form).then(res=>{
                                this.closeForm()
                                this.$message({
                                    type: 'success',
                                    message: res.data.message
                                });
                            })
                            break;
                            case 'add':
                            FileApi.addState(this.form).then(res=>{
                                this.closeForm()
                                this.$message({
                                    type: 'success',
                                    message: res.data.message
                                });
                            })
                            break;
                            default:
                        }
                    }
                }
             })
        },
        //关闭弹框
        closeForm(){
            this.$emit('close',false)
        },
        // ..........清空表单信息
        resetForm(formName) {
            this.loading = false
            this.$refs[formName].resetFields()
        },
        
    }
}
</script>

