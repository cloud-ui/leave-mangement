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
                        <li>3.您当月请假次数为：<span style="color:red">{{count}}</span> 次</li>
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
                    <el-form-item prop="value13" label="起止时间：">
                        <el-col :span="12">
                           <el-date-picker
                                v-model="form.value13"
                                type="daterange"
                                start-placeholder="开始日期"
                                end-placeholder="结束日期"
                                :default-time="['00:00:00', '23:59:59']">
                            </el-date-picker>
                        </el-col>
                        <el-col class="approval-date-leng" :span="6">您请假天数为：<span style="color:red">{{dateLeng}}</span> 天</el-col>
                    </el-form-item>
                    <el-form-item prop="account" label="请假理由：">
                        <el-input placeholder="50字以内" type="textarea" v-model="form.account"></el-input>
                    </el-form-item>
                    <el-form-item style="display: flex;justify-content: flex-end;">
                        <el-button @click="resetForm()">重置</el-button>
                        <el-button type="primary" @click="onSubmit(false)">保存</el-button>
                        <el-button type="primary" :loading="loading" @click="onSubmit(true)">提交</el-button>
                        
                    </el-form-item>
                </el-form>
            </el-card>
        </div>
    </div>
</template>
<script>
    import '../../index.scss'
    import '../approval.scss'
    import {mapGetters} from 'vuex'
    import {ApprovalApi} from '../api.js'
    export default {
        data(){
            var checkDateLeng=(rule, value, callback)=>{
                console.log(this.form.value13)
                if(this.form.value13.length !== 2){
                    return callback(new Error('时间未必填项'));
                }else if(!this.getDateLeng()){
                    return callback(new Error('请假天数不符合规定'));
                }
                callback();
            };
            return{
                form:{
                    name:'',
                    deparmentName:'开发部',
                    type1:1,
                    type2:0,
                    account:'',
                    startTime:'',
                    endTime:'',
                    isSubmit:'',
                    value13:[],
                },
                count:0,
                dateLeng:0,
                isEdit:'',
                loading:false,
                rules:{
                    type2:[{required: true,message: '请假类型为必选项',trigger: 'blur'}],
                    account:[{required: true,message: '请假理由为必填项',trigger: 'blur'}],
                    value13:[{required:true,trigger:'blur',validator: checkDateLeng,}]
                }
            }
        },
        created() {
            this.fetchData();
        },
        watch: {
            $route: "fetchData"
        },
        computed: {
            ...mapGetters([
                'userInfo'
            ])
        },
        methods: {
            //初始化表单
            fetchData(){
                //获取到地址栏传的参数
                const id = parseInt(this.$route.params.id);
                //-1为添加申请状态状态
                this.loadCount()
                if(id === -1){
                    this.isEdit = false
                    this.form.name = this.userInfo.name
                    ApprovalApi.getDeparment(this.userInfo.departmentId).then(res=>{
                        this.form.deparmentName = res.data.name
                    })
                }else{
                    this.isEdit = true
                    ApprovalApi.getApplication(id).then(res=>{
                        this.form = {...res.data}
                        this.form.value13[0] = this.formatDate(res.data.startTime)
                        this.form.value13[1] = this.formatDate(res.data.endTime)
                    })
                }
            },
            handleSelect(key, keyPath) {
                if(key === "1"){
                    this.form.type1 = 1
                }else if(key === "2"){
                    this.form.type1 = 2
                }
            },
            loadCount(){
                ApprovalApi.getApplicationCount().then(res=>{
                    this.count = res.data
                })
            },
            //提交
            onSubmit(isSubmit){
                this.$refs['form'].validate(valid=>{
                    if(valid){
                        this.loading = true
                        const params={
                            workerId:this.userInfo.id,
                            type1 : this.form.type1,
                            type2 : parseInt(this.form.type2),
                            account : this.form.account,
                            startTime:this.form.startTime,
                            endTime:this.form.endTime,
                            isSubmit:isSubmit,
                        }
                        if(!this.isEdit){
                            ApprovalApi.addApplication(params).then(res=>{
                            const type1 = res.data.isSuccess?'success':'error'
                            this.$message({
                                type:type1,
                                message:res.data.message
                            })
                            this.loading = false
                            const path = this.form.isSubmit?'/applicationList':'/unApplicationList'
                            this.$router.push({ path: path})
                        })
                        }else{
                            ApprovalApi.editApplication(params).then(res=>{
                                const type1 = res.data.isSuccess?'success':'error'
                                this.$message({
                                    type:type1,
                                    message:res.data.message
                                })
                                this.loading = false
                                const path = this.form.isSubmit?'/applicationList':'/unApplicationList'
                                this.$router.push({ path: path})
                            })
                        }
                        
                    }
                })
            },
            //重置表单
            resetForm(){
                this.form={
                    name:'',
                    deparmentName:'',
                    type1:1,
                    type2:0,
                    account:'',
                    startTime:'',
                    endTime:'',
                    isSubmit:'',
                }
            },
            //获取到请假的天数
            getDateLeng(){
                var dateDiff = this.form.value13[1].getTime() - this.form.value13[0].getTime();//时间差的毫秒数
                var dayDiff = Math.floor(dateDiff / (24 * 3600 * 1000));//计算出相差天数
                if((this.form.type2 === "2" && dayDiff > 7) ||(this.form.type2 === "3" && dayDiff > 15) ){
                    this.dateLeng = dayDiff
                    return false
                }else{
                    this.dateLeng = dayDiff
                    this.form.startTime = this.form.value13[0].getTime()
                    this.form.endTime = this.form.value13[1].getTime()
                    return true
                }                    
            },
            //转换时间格式
            formatDate(value) {
                let date = new Date(value);
                return date;
            }
        },
        
    }
</script>

