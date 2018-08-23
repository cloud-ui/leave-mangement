<template>
    <div class="index">
        <div class="index-title">
            <p>待审核申请</p>
        </div>
        <div class="index-body index-body1">
            <div class="index-body-menu">
                <el-menu default-active="1" class="el-menu-vertical-demo" @select="handleSelect">
                    <el-menu-item index="1">
                        <i class="el-icon-document"></i>
                        <span>请假申请</span>
                    </el-menu-item>
                    <el-menu-item index="2">
                        <i class="el-icon-setting"></i>
                        <span>转正/离职申请</span>
                    </el-menu-item>
                </el-menu>
            </div>
            <comp-applicationlist v-if="defaShow==='applicationlist'"></comp-applicationlist>
            <comp-apply v-if="defaShow==='applylist'"></comp-apply>
        </div>
    </div>
</template>
<script>
    import '../index.scss'
    import { CheckApi } from "./api.js"
    import CompApplicationlist from './applicationlist'
    import CompApply from './applylist'
    export default {
        components:{
            CompApplicationlist,
            CompApply
        },
        data() {
            return {
                defaShow:'applicationlist'
            }
        },
        methods: {
            handleSelect(key, keyPath) {
                switch(key){
                    case "1":
                    this.defaShow = 'applicationlist'
                    break;
                    case "2":
                    this.defaShow = 'applylist'
                    break;
                }
            },
        },
    }
</script>
<style lang="scss" scoped>
    .link {
        padding: 0px 0px 0px 10px;
        font-size: 14px;
        color: #409eff;
        text-decoration: none;
    }
</style>

