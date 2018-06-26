<template>
<div>
  <div class="add-head">
      <div class="head-container">
          <div style="padding-top:15px;">
            <p class="head-title">人事考勤系统</p>
            <span class="add-company">添加公司</span>
          </div>
      </div>
  </div>  
  <div class="add-body">
      <div class="body-container">
          <div class="step-bar">
              <ol>
                  <li >
                      <div class="step-div step-bar-active"></div>
                  </li>
                  <li class="step-bar-active">
                      <div v-if="step==='SecondStep'" class="step-div step-bar-active"></div>
                      <div v-else class="step-div"></div>
                  </li>
                  <li>
                      <div v-if="step==='Resule'" class="step-div step-bar-active"></div>
                      <div v-else class="step-div"></div>
                  </li>
              </ol>
          </div>
          <div class="add-box">               
              <component v-bind:is="step"></component>
          </div>
      </div>
  </div>
</div>
</template>
<script>
import './add.scss';
import FirstStep from './firstStep'
import SecondStep from './secondStep'
import { mapState } from 'vuex'
export default{
   components:{
       FirstStep,
       SecondStep
   },
   watch: {
      show: {
        handler(val, olaval) {
          this.show = val.show;
        },
        deep: true
      }
    },
   data(){
       return{           
       }
   },
   computed:mapState({
           step: state=>state.addCompany.step
       }),
}
</script>
<style lang="scss" scoped>

</style>
