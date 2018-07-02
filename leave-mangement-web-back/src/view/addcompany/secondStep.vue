<template>
  <div id="second">
        <!-- <el-form v-model="companyMessage" label-width="140px" class="demo-ruleForm">
            <el-form-item label="选择员工职位状态：">
                <el-checkbox-group v-model="companyMessage.states">
                    <el-checkbox label="在职" name="type"></el-checkbox>
                    <el-checkbox label="外派" name="type"></el-checkbox>
                    <el-checkbox label="实习" name="type"></el-checkbox>
                    <el-checkbox label="兼职" name="type"></el-checkbox>
                    <el-checkbox label="离职" name="type"></el-checkbox>
                </el-checkbox-group>
            </el-form-item>
            <el-form-item label="验证码">
                <el-input style="width:32%;"></el-input>
                <el-button>获取验证码</el-button>
            </el-form-item>
            <el-form-item>
                <el-button type="primary" @click="back()">上一步</el-button>
                <el-button @click="submitCompany()">提交</el-button>
            </el-form-item>
        </el-form> -->
        <div v-if="result.isSuccess" class="show">
            <i style="color:#5fb878;font-size:40px;" class="el-icon-success"></i>
            <span>您的公司已添加成功！账号密码已发入您的邮箱，请注意查收！</span>
            <router-link to="/login">前往登录</router-link>
        </div>
        <div class="show" v-if="!result.isSuccess">
            <i style="color:red;font-size:40px;" class="el-icon-error"></i>
            <span>添加公司失败！</span>
        </div>
   </div>
</template>
<script>
import {mapState, mapActions} from "vuex"
export default {
    props:['result'],
    data(){
        return {
            phone:'',
        }
    },
//     computed:mapState({
//             companyMessage:state=>state.addCompany.companyMessage
//         }),
//   methods:{ 
//       ...mapActions([
//             'backStep',
//         ]),
//       back(){
//           this.backStep('FirstStep')
//       }
//   }
}
</script>
<style lang="scss" scoped>
.show{
    display: flex;
    align-items: center
}
</style>


