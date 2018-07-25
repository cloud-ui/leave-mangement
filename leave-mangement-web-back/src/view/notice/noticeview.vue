<template>
    <div>
        <div>
            {{this.notice.content}}
        </div>
        <div style="text-align: right;">
            <p>{{this.notice.createTime}}</p>
            <p>{{this.notice.createHuman}}</p>
        </div>
    </div>
</template>
<script>
export default {
    props:['notice']
}
</script>
