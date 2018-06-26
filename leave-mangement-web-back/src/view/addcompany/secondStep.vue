<template>
  <div id="second">
        <el-form v-model="companyMessage" label-width="140px" class="demo-ruleForm">
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
        </el-form>
   </div>
</template>
<script>
import {mapState, mapActions} from "vuex"
export default {
    data(){
        return {
            phone:'',
        }
    },
    computed:mapState({
            companyMessage:state=>state.addCompany.companyMessage
        }),
  methods:{ 
      ...mapActions([
            'backStep',
        ]),
      back(){
          this.backStep('FirstStep')
      }
  }
}
</script>

