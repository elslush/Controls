export function initSlider(slider) {
    slider.querySelectorAll('.slider-node').forEach(node => {
        const nodeWidth = node.offsetWidth;
        node.style.left = -nodeWidth + 'px';
        node.style.marginLeft = (nodeWidth / 2) + 'px';
        node.addEventListener('mousedown', evt => {
            evt.preventDefault();
            evt.stopPropagation();
            const sliderLeft = slider.offsetLeft;
            const sliderWidth = slider.offsetWidth;
            const sliderOffset = sliderLeft + sliderWidth;

            function handleMouseMove(evt) {
                evt.preventDefault();
                evt.stopPropagation();
                console.log(evt.pageX);
                if (evt.pageX >= sliderLeft && evt.pageX <= sliderOffset) {
                    node.style.left = `${evt.pageX - sliderLeft - nodeWidth}px`;
                }
            }

            function handleMouseUp() {
                document.body.removeEventListener('mousemove', handleMouseMove);
                document.body.removeEventListener('mouseup', handleMouseUp);
            }

            document.body.addEventListener('mousemove', handleMouseMove);
            document.body.addEventListener('mouseup', handleMouseUp);
        });
    });
}