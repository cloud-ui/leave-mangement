<template>
  <div id="second">
        <el-form label-width="120px" class="demo-ruleForm">
            <el-form-item label="手机号码">
                <el-input v-model="phone" style="width:50%;"></el-input>
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

