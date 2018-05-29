    <template>
    <div id="first">
        <el-form :model="companyMessage" :rules="rules" ref="companyMessage" :inline-message="true" label-width="120px" class="demo-ruleForm">
            <el-form-item label="公司名称" prop="name">
                <el-input style="width:50%" v-model="companyMessage.name"></el-input>                      
            </el-form-item>
            <el-form-item label="公司地址" prop="address">
                <el-input style="width:50%" v-model="companyMessage.address"></el-input>
            </el-form-item>
            <el-form-item label="公司法人" prop="corporation">
                <el-input style="width:30%" v-model="companyMessage.corporation"></el-input>
            </el-form-item>
            <el-form-item label="公司成立时间" prop="createTime">
                <el-date-picker v-model="companyMessage.createTime" type="date" placeholder="选择日期" style="width: 30%;"></el-date-picker>
            </el-form-item>
            <el-form-item label="公司固定电话" prop="cellPhoneNumber">
                <el-input style="width:50%" v-model="companyMessage.cellPhoneNumber"></el-input>
            </el-form-item>
            <el-form-item label="邮箱" prop="email">
                <el-input style="width:50%" v-model="companyMessage.email"></el-input>
            </el-form-item>
            <el-form-item label="验证码">
                <el-input prop="authCode" style="width:32%;"></el-input>
                <el-button :disabled="disable" @click="getAuthCode(companyMessage.email)">{{getAuthCodeText}}</el-button>
            </el-form-item>
            <el-form-item>
                <!-- <el-button type="primary" @click="next()">下一步</el-button> -->
                <el-button type="primary" :loading="loading">提交</el-button>
                <el-button @click="resetForm('companyMessage')">重置</el-button>
            </el-form-item>
        </el-form>
    </div>
    </template>
    <script>
    import './add.scss';
    import {mapState, mapActions} from "vuex"
    import {AddCompanyApi} from './api.js'
    export default {
       data (){
        var checkEmail = (rule, value, callback) => {
            if (value) {
                this.disable = false;
                 }
        };     
        return{
            disable:true,
            authTime:0,
            getAuthCodeText:'获取验证码',
            loading:false,   
            rules: {
                name: [{ required: true, message: '请输入活动名称', trigger: 'blur' }],
                address:[{required:true,message:'请输入公司地址',trigger:'blur'}],
                corporation:[{required:true,message:'请输入公司法人',trigger:'blur'}],
                createTime:[{required:true,message:'请输入公司成立时间',trigger:'blur'}],
                cellPhoneNumber:[{required:true,message:'请输入公司固定电话',trigger:'blur'}],
                email:[
                    {required:true,message:'请输入邮箱',trigger: 'change'},
                    {validator:checkEmail,trigger:'change'}
                    ],
                authCode:[{required:true,message: '请输入活动名称', trigger: 'blur'}]
                },
            
        }
        },
        computed:mapState({
            companyMessage:state=>state.addCompany.companyMessage
        }),
        methods: {
            ...mapActions([
            'nextStep',
        ]),
        next() {
            const data = {
                step:'SecondStep',
            }
        this.nextStep(data)
        },
        resetForm(formName) {
            this.$refs[formName].resetFields();
        },
        getAuthCode(email){
            this.disable = true;
            this.getAuthTime();
            AddCompanyApi.getAuthCode(email).then(res=>{
              alert("chenggong")
            })
        },
        getAuthTime(){
            this.authTime = 120;
            var authTimetimer = setInterval(()=>{
                this.authTime--;
                this.getAuthCodeText=this.authTime+"重新获取验证码";
                if(this.authTime<=0){
                    this.disable = false;
                    this.getAuthCodeText="获取验证码";
                    clearInterval(authTimetimer);
                }
            }, 1000);
        }
        }
        }
    </script>
    
