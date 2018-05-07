<template>
  <div class="login">
    <div class="background">
    </div>
    <el-form :model="ruleForm" :rules="rules" ref="ruleForm" label-position="left" label-width="0px"
             class="demo-ruleForm login-container">
      <h3 class="login-title">系统登录</h3>
      <el-form-item prop="loginStr">
        <el-input
          type="text"
          v-model="ruleForm.loginStr"
          ref="adminAccount"
          auto-complete="off"
          placeholder="请输入用户名">
        </el-input>
      </el-form-item>
      <el-form-item prop="password">
        <el-input
          type="password"
          v-model="ruleForm.password"
          auto-complete="off"
          placeholder="请输入密码"
        >
        </el-input>
      </el-form-item>
      <el-form-item>
        <el-button
          style="width: 100%;"
          type="primary"
          @click.native.prevent="submitForm"
          :loading="loading">登录
        </el-button>
      </el-form-item>
    </el-form>
    
  </div>
</template>

<script>

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
          loginStr: [
            {required: true, message: '请输入登录账户', trigger: 'blur'},
            {min: 6, max: 12, message: '长度在 6 到 12 个字符', trigger: 'blur'}
          ],
          password: [
            {required: true, message: '请输入登录密码', trigger: 'blur'}
          ],
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
      // enter按键事件
      Enter() {
        document.onkeydown = (e) => {
          if (e.which === 13) {
            this.submitForm()
          }
        }
      },
      getUser() {
        this.ruleForm = this.$store.getters.accountPwd
      },
      submitForm() {
        this.$refs['ruleForm'].validate((valid) => {
          if (valid) {
            // this.loading = true;
            // this.$store.dispatch('accountLoginSubmit',
            //   {...this.ruleForm}).then(() => {
            //   this.loading = false;
            //   this.$router.push('/admin')
            // }).catch(() => {
            //   this.loading = false
            // })
          } else {
            return false
          }
        })
      },
    },
    /** 监听函数 */
    watch: {
      $route() {
        this.path = this.$route.path.split('/')[2]
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
    &-title {
      margin-bottom: 10px;
      text-align: center;
    }
  }
  .background{
    width: 100%;
    height: 100%;
    background-color: #fff;
    opacity: 0.6;
    background-repeat: no-repeat;
    background-size: cover;
    background-position: center;
  }

  .login-container {
    -webkit-border-radius: 5px;
    border-radius: 5px;
    -moz-border-radius: 5px;
    background-clip: padding-box;
    width: 350px;
    padding: 35px 35px 15px 35px;
    background: #fff;
    border: 1px solid #eaeaea;
    box-shadow: 0 0 25px #cac6c6;
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -60%);
    overflow: hidden;
    min-width: 270px;
    .title {
      text-align: center;
      color: #505458;
    }
  }

  .login-footer {
    text-align: center;
  }
</style>
