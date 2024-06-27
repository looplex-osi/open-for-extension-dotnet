module.exports = {
    branches: ['main'],
    tagPrefix: 'v',
    changelogFile: 'CHANGELOG.md',
    bumpFiles: [
        {
            filename: 'scr/Directory.Build.props',
            type: 'xml',
            search: '<Version>',
            replace: '<Version>${version}</Version>'
        }
    ],
    commitMessage: 'chore(release): ${version}',
    tagName: 'v${version}',
    changelog: {
        enabled: true,
        commitTypes: {
            feat: 'Features',
            fix: 'Bug Fixes',
            docs: 'Documentation',
            style: 'Styles',
            refactor: 'Code Refactoring',
            perf: 'Performance Improvements',
            test: 'Tests',
            build: 'Builds',
            ci: 'Continuous Integration',
            chore: 'Chores',
            revert: 'Reverts'
        }
    }
};