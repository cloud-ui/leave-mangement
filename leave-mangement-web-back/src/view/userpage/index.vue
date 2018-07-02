<template>
    <div class="index">
        <div class="index-title">
            <p>个人中心</p>
        </div>
        <div class="index-body index-body1">
            <div class="index-body-menu">
                <el-menu default-active="1" class="el-menu-vertical-demo" @select="handleSelect">
                    <el-menu-item index="1">
                        <i class="el-icon-document"></i>
                        <span>个人资料</span>
                    </el-menu-item>
                    <el-menu-item index="3">
                        <i class="el-icon-date"></i>
                        <span>出勤统计</span>
                    </el-menu-item>
                </el-menu>
            </div>
            <comp-userinfo v-if="defaShow === 'userInfo'"></comp-userinfo>
            <comp-attence v-if="defaShow === 'attence'"></comp-attence>
        </div>
    </div>
</template>
<script>
    import '../index.scss'
    import CompUserinfo from './userinfo'
    import CompAttence from './attence'
    export default {
        components: {
            CompUserinfo,
            CompAttence
        },
        data() {
            return {
                companyInfo: {},
                defaShow: 'userInfo',
            }
        },
        methods: {
            handleSelect(key, keyPath) {
                switch(key){
                    case "1":
                    this.defaShow = 'userInfo'
                    break;
                    case "3":
                    this.defaShow = 'attence'
                }
            },
        }
    }
</script>
<style lang="scss" scoped>

</style>


