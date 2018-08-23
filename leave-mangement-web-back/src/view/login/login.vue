<template>
  <div class="login">
    <!-- <div class="background">
    </div> -->
    <div class="login-body">
      <div class="login-body-left">
        <h3>公司人事管理系统</h3>
        <p>√ 是一个基于ASP.NET Core开发基于Vue前后分离的开发平台</p>
        <p>√ 是一个基于vue+vuex+vue-router快速后台管理系统，采用token交互验证方式。</p>
        <p>√ 最大程度上帮助企业节省时间成本和费用开支。</p>
      </div>
      <div class="login-body-right postion">
        <el-form :model="ruleForm" :rules="rules" ref="ruleForm" label-position="left" label-width="0px" class="demo-ruleForm login-container">
          <p class="login-title">公司人事管理系统登录</p>
          <el-form-item style="padding-top:15px" prop="loginStr">
            <el-input type="text" v-model="ruleForm.loginStr" ref="adminAccount" auto-complete="off" placeholder="请输入用户名">
            </el-input>
          </el-form-item>
          <el-form-item prop="password">
            <el-input type="password" v-model="ruleForm.password" auto-complete="off" placeholder="请输入密码">
            </el-input>
          </el-form-item>
          <el-form-item class="login-link">
            <router-link style="font-size: 5px;color: #409eff;text-decoration: none;" to="/addCompany">没有账号？注册公司</router-link>
          </el-form-item>
          <el-form-item>
            <el-button style="width: 100%;" type="primary" @click.native.prevent="submitForm" :loading="loading">登录
            </el-button>
          </el-form-item>
        </el-form>
      </div>
    </div>
    
    
  </div>
</template>

<script>
  import {LoginApi} from './api.js'
  import {mapActions} from "vuex"
  export default {
    name: 'Login',
    components: {},
    /** state 默认信息 */
    data() {
      return {
        loading: false,
        ruleForm: {
          loginStr: '',
          password: '',
        },
        // 表单数据规则
        rules: {
          loginStr: [{
              required: true,
              message: '请输入登录账户',
              trigger: 'blur'
            },
            {
              min: 6,
              max: 12,
              message: '长度在 6 到 12 个字符',
              trigger: 'blur'
            }
          ],
          password: [{
            required: true,
            message: '请输入登录密码',
            trigger: 'blur'
          }],
        }
      }
    },
    created() {
      window.addEventListener('keydown', this.Enter);
    },
    /** 计算属性 */
    computed: {},
    /** 完成挂载 */
    mounted() {
      this.$refs['adminAccount'].focus();
    },
    destroyed() {
      window.removeEventListener('keydown', this.Enter)
    },
    /** 方法事件 */
    methods: {
      ...mapActions([
        'setUser'
      ]),
      // enter按键事件
      Enter() {
        document.onkeydown = (e) => {
          if (e.which === 13) {
            this.submitForm()
          }
        }
      },
      submitForm() {
        this.$refs['ruleForm'].validate((valid) => {
          if (valid) {
            this.loading = true;
            const param = {
              account: this.ruleForm.loginStr,
              password: this.ruleForm.password
            }
            LoginApi.login(param).then(res => {
              this.loading = false;
              this.setUser(res.data)
              this.$router.push({
                path: '/home'
              })
            })
          } else {
            return false
          }
        })
      },
    },
    /** 监听函数 */
    watch: {
      $route() {
        this.path = this.$route.path.split('/home')[2]
      }
    }
  }
</script>

<style lang="scss" scoped>
  .login {
    width: 100%;
    height: 100%;
    background-image: url("../../assets/images/loginImg.jpg");
    background-repeat: no-repeat;
    background-size: cover;
    background-position: center;
    min-width: 1000px;
    .el-form-item {
      .el-form-item__content {
        .icon {
          position: absolute;
          left: 8px;
          z-index: 1;
        }
      }
    }
  }
  .login-body{
    width: 100%;
    height: 100%;
    display: flex;
    justify-content: flex-start;
    background-color: rgba(0,0,0,.1);
    &-left{
      width:50%;
      margin: auto;
      text-align: left;
      margin-left: 10%;
      >h3{
        color:#fff;
        padding-bottom: 15px;
        letter-spacing: 3px;
      }
      >p{
        color:#fff;
        padding-bottom: 5px;
      }
    }
    &-right{
      width: 50%;
    }
  }
  .postion{
    display: flex;
    align-items: center;
    justify-content: center;
  }
  .login-container {
    -webkit-border-radius: 5px;
    border-radius: 10px;
    -moz-border-radius: 5px;
    background-clip: padding-box;
    width: 270px;
    padding: 25px 25px 0px 25px;
    background: #fff;
    border: 1px solid #eaeaea;
    box-shadow: 0 0 25px #cac6c6;
    position: absolute;
    overflow: hidden;
    min-width: 270px;
    &-type{
      border-bottom: 2px solid #1C3B5B;
      color: #1C3B5B;
      font-size: 14px;
      padding-bottom: 5px;
    }
  }
  .login-title {
      text-align: center;
      color: #000;
      font-weight: 600;
      letter-spacing: 2px;
      padding-bottom: 15px;
    }
  .login-link{
    margin: 0px;
    padding: 0px;
    margin-top: -15px;
    display: flex;
    justify-content: flex-end;
  }
</style>
