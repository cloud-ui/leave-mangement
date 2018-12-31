<template>
    <el-form ref="addForm" :model="form" :rules="rule" label-width="80px">
        <el-form-item v-if="this.formInfo.state === 'position'" label="上级：">
            <el-select v-model="form.parentId">
                <el-option
                    v-for="item in SelectData"
                    :key="item.id"
                    :label="item.name"
                    :value="item.id">
                </el-option>
            </el-select>
        </el-form-item>
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
            SelectData:[],
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
    mounted(){
        this.loadSelectData()
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
                            FileApi.editPosition(params).then(res=>{
                                this.closeForm()
                                this.$message({
                                    type: 'success',
                                    message: res.data.message
                                });
                            })
                            break;
                            case 'add':
                            FileApi.addPosition(params).then(res=>{
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
        loadSelectData(){
            this.SelectData=[{
                id:0,
                name:'最高层'
            }]
            FileApi.getPositionList().then(res => {
                    this.SelectData = res.data
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

