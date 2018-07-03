    <template>
    <div id="first">
        <el-form :model="companyMessage" :rules="rules" ref="companyMessage" :inline-message="true" label-width="120px" class="demo-ruleForm">
            <el-form-item label="公司名称:" prop="name">
                <el-input style="width:50%" v-model="companyMessage.name"></el-input>                      
            </el-form-item>
            <el-form-item label="公司地址:" prop="address">
                <!-- <el-input style="width:50%" v-model="companyMessage.address"></el-input> -->
                <el-col :span="14"><city-select v-model="cityInfo"></city-select></el-col>
                <el-col :span="1">--</el-col>
                <el-col :span="9"><el-input v-model="smallAddress" placeholder="详细地址"></el-input></el-col>
            </el-form-item>
            <el-form-item label="公司法人:" prop="corporation">
                <el-input style="width:30%" v-model="companyMessage.corporation"></el-input>
            </el-form-item>
            <el-form-item label="公司成立时间:" prop="createTime">
                <el-date-picker v-model="companyMessage.createTime" type="date" placeholder="选择日期" style="width: 30%;"></el-date-picker>
            </el-form-item>
            <el-form-item label="公司固定电话:" prop="cellPhoneNumber">
                <el-input style="width:50%" v-model="companyMessage.cellPhoneNumber"></el-input>
            </el-form-item>
            <el-form-item label="邮箱:" prop="email">
                <el-input style="width:50%" v-model="companyMessage.email"></el-input>
            </el-form-item>
            <el-form-item label="验证码:">
                <el-input v-model="authCode" style="width:32%;"></el-input>
                <el-button :disabled="disable" @click="getAuthCode(companyMessage.email)">{{getAuthCodeText}}</el-button>
            </el-form-item>
            <el-form-item>
                <!-- <el-button type="primary" @click="next()">下一步</el-button> -->
                <el-button @click="handleSubmit()" type="primary" :loading="loading">提交</el-button>
                <el-button @click="resetForm('companyMessage')">重置</el-button>
            </el-form-item>
        </el-form>
    </div>
    </template>
    <script>
    import './add.scss';
    import {mapState, mapActions} from "vuex"
    import {AddCompanyApi} from './api.js'
    import CitySelect from '../../packages/components/addressSelector'
    export default {
       data (){
        var checkEmail = (rule, value, callback) => {
            if (value) {
                this.disable = false;
                 }
        };     
        return{
            cityInfo: '',
            disable:true,
            authTime:0,
            authCode:'',
            sendAuthCode:'',
            getAuthCodeText:'获取验证码',
            loading:false, 
            smallAddress:'',
            result:{},  
            rules: {
                name: [{ required: true, message: '请输入活动名称', trigger: 'blur' }],
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
        components: {
            CitySelect
        },
        computed:{
            companyMessage(){
                return this.$store.state.addCompany.companyMessage
            },
            cityName() {
                const names = [];
                this.cityInfo.province && names.push(this.cityInfo.province.name + ' ')
                this.cityInfo.city     && names.push(this.cityInfo.city.name + ' ')
                this.cityInfo.block    && names.push(this.cityInfo.block.name + ' ')
                return names.join('')
            }
        },
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
              this.sendAuthCode = res.data.authCode
            })
        },
        handleSubmit(){
            this.companyMessage.address = this.cityName+this.smallAddress
            console.log(this.companyMessage)
            if(this.authCode === this.sendAuthCode){
                AddCompanyApi.addCompany(this.companyMessage).then(res=>{
                    this.$emit('change',{...res.data})
                })
            }else{
                this.$message.error('验证码错误');
            }
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
    
