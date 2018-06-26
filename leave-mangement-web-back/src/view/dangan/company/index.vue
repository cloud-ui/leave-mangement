<template>
    <div class="index">
        <div class="index-title">
            <p>公司档案</p>
        </div>
        <div class="index-body index-body1">
            <div class="index-body-menu">
                <el-menu default-active="1" class="el-menu-vertical-demo" @select="handleSelect">
                    <el-menu-item index="1">
                        <i class="el-icon-document"></i>
                        <span slot="title">公司资料</span>
                    </el-menu-item>
                    <el-menu-item index="2">
                        <i class="el-icon-setting"></i>
                        <span slot="title">职位管理</span>
                    </el-menu-item>
                    <el-menu-item index="3">
                        <i class="el-icon-setting"></i>
                        <span slot="title">员工状态管理</span>
                    </el-menu-item>
                </el-menu>
            </div>
            <component v-bind:is="defaShow"></component>
        </div>
    </div>
</template>
<script>
    import {
        FileApi
    } from '../api.js'
    import CompCompany from './company'
    import CompPosition from './position'
    import CompWorkerState from './workerState'
    import '../dangan.scss';
    import '../../index.scss'
    export default {
        components: {
            CompCompany,
            CompPosition,
            CompWorkerState
        },
        data() {
            return {
                companyInfo: {},
                defaShow: 'CompCompany',
            }
        },
        methods: {
            handleSelect(key, keyPath) {
                switch(key){
                    case '1':
                    this.defaShow = 'CompCompany'
                    break;
                    case '2':
                    this.defaShow = 'CompPosition'
                    break;
                    case '3':
                    this.defaShow = 'CompWorkerState'
                }
            },
        }
    }
</script>
<style lang="scss" scoped>

</style>


