<template>
    <div class="index">
        <div class="index-title">
            <p>填写请假申请</p>
        </div>
        <div class="index-body index-body1">
            <div class="index-body-menu">
                <el-menu default-active="1" class="el-menu-vertical-demo" @select="handleSelect">
                    <el-menu-item index="1">
                        <i class="el-icon-tickets"></i>
                        <span slot="title">事前请假</span>
                    </el-menu-item>
                    <el-menu-item index="2">
                        <i class="el-icon-document"></i>
                        <span slot="title">事后补假</span>
                    </el-menu-item>
                </el-menu>
            </div>
            <el-card style="width:82%" class="box-card">
                <div class="add-tip">
                    <ul>
                        <li>1.请假类型之间不能组合</li>
                        <li>2.请假天数限制：事假不得超过7天，年假不得超过15天</li>
                        <li>3.您当月请假次数为：<span style="color:red">0</span> 次</li>
                    </ul>
                </div>
                <el-form label-position="right" style="padding-top:20px;" ref="form" :rules="rules" :model="form" label-width="100px">
                    <el-form-item label="员工姓名：">
                        <el-col :span="10"><el-input disabled v-model="form.name"></el-input></el-col>
                    </el-form-item>
                    <el-form-item label="员工部门：">
                        <el-col :span="10"><el-input disabled v-model="form.deparmentName"></el-input></el-col>
                    </el-form-item>
                    <el-form-item prop="type2" label="请假类型：">
                        <el-radio-group v-model="form.type2">
                            <el-radio label=2 >事假</el-radio>
                            <el-radio label=1>病假</el-radio>
                            <el-radio label=3>年假</el-radio>
                        </el-radio-group>
                    </el-form-item>
                    <el-form-item label="起止时间：">
                        <el-col :span="8">
                            <el-date-picker type="date" placeholder="选择开始日期" v-model="form.startTime" style="width: 100%;"></el-date-picker>
                        </el-col>
                        <el-col class="line" :span="1">------</el-col>
                        <el-col :span="8">
                            <el-date-picker type="date" @change="getDateLeng()" placeholder="选择结束时间" v-model="form.endTime" style="width: 100%;"></el-date-picker>
                        </el-col>
                        <el-col class="approval-date-leng" :span="6">您请假天数为：<span style="color:red">{{dateLeng}}</span> 天</el-col>
                    </el-form-item>
                    <el-form-item prop="account" label="请假理由：">
                        <el-input placeholder="50字以内" type="textarea" v-model="form.account"></el-input>
                    </el-form-item>
                    <el-form-item style="display: flex;justify-content: flex-end;">
                        <el-button @click="resetForm()">重置</el-button>
                        <el-button type="primary" @click="onSubmit(false)">保存</el-button>
                        <el-button type="primary" @click="onSubmit(true)">提交</el-button>
                        
                    </el-form-item>
                </el-form>
            </el-card>
        </div>
    </div>
</template>
<script>
    import '../index.scss'
    import './approval.scss'
    export default {
        data(){
            return{
                form:{
                    name:'',
                    deparmentName:'',
                    type1:1,
                    type2:0,
                    account:'',
                    startTime:'',
                    endTime:'',
                    isSubmit:'',
                },
                dateLeng:0,
                rules:{
                    type2:[{required: true,message: '请假类型为必选项',trigger: 'blur'}],
                    startTime:[{required: true,message: '起始时间为必选项',trigger: 'blur'}],
                    account:[{required: true,message: '请假理由为必填项',trigger: 'blur'}]
                }
            }
        },
        methods: {
            handleSelect(key, keyPath) {
                if(key === "1"){
                    this.form.type1 = 1
                }else if(key === "2"){
                    this.form.type1 = 2
                }
            },
            //提交
            onSubmit(isSubmit){
                this.$refs['form'].validate(valid=>{
                    if(valid){
                        const params={
                            type1 : this.form.type1,
                            type2 : parseInt(this.form.type2),
                            account : this.form.account,
                            statrTime:this.form.startTime,
                            endTime:this.form.endTime,
                            isSubmit:isSubmit,
                        }
                        console.log(params)
                    }
                })
            },
            //重置表单
            resetForm(){
                this.$refs['form'].resetFields()
                this.form.startTime=''
                this.form.endTime=''
            },
            //获取到请假的天数
            getDateLeng(){
                console.log(this.form)
                var dateDiff = this.form.endTime.getTime() - this.form.startTime.getTime();//时间差的毫秒数
                var dayDiff = Math.floor(dateDiff / (24 * 3600 * 1000));//计算出相差天数
                this.dateLeng = dayDiff
            }
        },
        
    }
</script>

